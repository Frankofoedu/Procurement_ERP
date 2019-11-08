using DcProcurement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Pages.Vendor
{
    //TODO: Cheeck for errors if any returned entity doesnt and display error meesage

    public class DocsModel
    {
        public int Id { get; set; }
        public bool isDoc { get; set; }
        public IFormFile Image { get; set; }
        public string FileName { get; set; }
        public string Value { get; set; }
    }

    public class PrequalificationModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<PrequalificationModel> _logger;
        private readonly UserManager<User> _userManager;

        public readonly IHostingEnvironment _hostingEnvironment;

        public PrequalificationModel(ProcurementDBContext context,
                                    IHostingEnvironment hostingEnvironment,
                                    UserManager<User> userManager,
                                    SignInManager<User> signInManager,
                                    ILogger<PrequalificationModel> logger)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public List<DocsModel> DocsList { get; set; }

        [BindProperty]
        public CompanyInfo CompanyInfo { get; set; }

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public List<PersonnelDetailInput> PersonnelDetailIntputs { get; set; }//add method to convert iformfiles to file paths for personnel details table

        [BindProperty]
        public List<EquipmentDetails> EquipmentDetails { get; set; }

        [BindProperty]
        public List<ExperienceRecord> ExperienceRecords { get; set; }

        [BindProperty]
        public List<CompanyInfoProcurementSubCategory> SelectedSubcategories { get; set; }

        public class PersonnelDetailInput
        {
            public string Name { get; set; }
            public string Qualification { get; set; }
            public IFormFile CVFile { get; set; }
            public IFormFile CertFile { get; set; }
            public IFormFile PassportFile { get; set; }
        }

        public string Message { get; set; }
        public string Error { get; set; }
        public int? CategoryCount { get; set; }

        [BindProperty]
        public List<int> subCatsId { get; set; }

        public void OnGet()
        {
            // gets the number of categories from the setup page
            var setup = _context.PrequalificationPolicies.FirstOrDefault();
            if (setup == null)
            {
                Error = "Prequalification has not yet started";
                return;
            }
            CategoryCount = setup.NoOfCategory;

            GetCategories();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            // CompanyInfo.SubCategories = GetSubCategories(subCatsId);

            if (!ModelState.IsValid)
            {
                Message = "No of personnel = " + PersonnelDetailIntputs.Count;
                CategoryCount = _context.PrequalificationPolicies.FirstOrDefault().NoOfCategory;
                GetCategories();
                return Page();
            }

            //sets date of creation
            CompanyInfo.CreationDate = DateTime.Now;

            //saves company to db
            _context.CompanyInfo.Add(CompanyInfo);

            await _context.SaveChangesAsync();

            //saves list of submitted criterias to db
            _context.SubmittedCriteria.AddRange(GetSubmittedCriterias(CompanyInfo.Id));

            AddEquipmentsToDB(CompanyInfo.Id, EquipmentDetails);

            AddExperienceToDB(CompanyInfo.Id, ExperienceRecords);

            AddSelectedSubcategoriesToDB(CompanyInfo.Id, SelectedSubcategories);

            AddPersonnelDetailsToDB(PersonnelDetailIntputs, CompanyInfo.Id);

            AddPrequalificationJob(CompanyInfo.Id);

            AddCompanyToJobs(CompanyInfo.Id);

            await _context.SaveChangesAsync();

            var rtn = await SignUpUserAsync(CompanyInfo);
            if (rtn == "error")
            {
                Error = "An Error occured!";
                return Page();
            }
            CompanyInfo.VendorId = rtn.Replace("userId=", "");

            await _context.SaveChangesAsync();

            Message = "Your Information has been successfully uploaded";

            return null; //change to redirect location
        }

        /// <summary>
        /// loads the categories
        /// </summary>
        private void GetCategories()
        {
            Categories = _context.ProcurementCategories
               .Select(a =>
                           new SelectListItem
                           {
                               Value = a.Id.ToString(),
                               Text = a.Name
                           })
               .ToList();
        }

        private List<ProcurementSubcategory> GetSubCategories(List<int> ids)
        {
            var subCats = _context.ProcurementSubcategories.Where(p => ids.Contains(p.Id)).ToList();

            if (subCats.Any())
            {
                return subCats;
            }

            return null;
        }

        //update docs model filenames
        private void GetImageFileName(List<DocsModel> formFiles, int id)
        {
            if (formFiles.Any())
            {
                foreach (var doc in formFiles)
                {
                    //check if it requires a document upload
                    if (doc.isDoc)
                    {
                        var fileName = doc.Image.FileName;
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Criteriadocs", id.ToString());

                        //checks if path exists, if not create it,id.ToString()
                        Directory.CreateDirectory(uploads);

                        var filePath = Path.Combine(uploads, fileName);
                        doc.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                        doc.FileName = fileName;
                    }
                }

                //    return fileNames;
            }

            // return null;
        }

        /// <summary>
        /// creates a list of submitted criterias and there docs paths
        /// </summary>
        /// <param name="id">Company Id</param>
        private List<SubmittedCriteria> GetSubmittedCriterias(int id)
        {
            GetImageFileName(DocsList, id);

            var SubmittedCriterias = new List<SubmittedCriteria>();

            foreach (var docs in DocsList)
            {
                if (docs.isDoc)
                {
                    SubmittedCriterias.Add(new SubmittedCriteria { CompanyInfoId = id, CriteriaId = docs.Id, Value = docs.FileName });
                }
                else
                {
                    SubmittedCriterias.Add(new SubmittedCriteria { CompanyInfoId = id, CriteriaId = docs.Id, Value = docs.Value });
                }
            }

            return SubmittedCriterias;
        }

        /// <summary>
        /// method for saving experience record using company id
        /// </summary>
        /// <param name="id">Id of the company to be registered</param>
        private void AddExperienceToDB(int id, List<ExperienceRecord> experienceRecords)
        {
            experienceRecords.ForEach(x => x.CompanyInfoId = id);

            _context.ExperienceRecord.AddRange(experienceRecords);
        }

        /// <summary>
        /// method for saving equipments details using company id
        /// </summary>
        /// <param name="id">id of the company</param>
        /// <param name="experienceRecords">list of custom experience records model</param>
        private void AddEquipmentsToDB(int id, List<EquipmentDetails> equipmentDetails)
        {
            equipmentDetails.ForEach(x => x.CompanyInfoId = id);

            _context.EquipmentDetails.AddRange(equipmentDetails);
        }

        /// <summary>
        /// method for saving equipments details using company id
        /// </summary>
        /// <param name="id">id of the company</param>
        /// <param name="experienceRecords">list of custom experience records model</param>
        private void AddSelectedSubcategoriesToDB(int id, List<CompanyInfoProcurementSubCategory> selectedSubCategorys)
        {
            selectedSubCategorys = selectedSubCategorys.DistinctBy(i => i.ProcurementSubcategoryId).ToList();

            var SubCs = new List<CompanyInfoProcurementSubCategory>();

            foreach (var item in selectedSubCategorys)
            {
                if (item.ProcurementSubcategoryId != 0)
                {
                    SubCs.Add(new CompanyInfoProcurementSubCategory()
                    {
                        CompanyInfoId = id,
                        ProcurementSubcategoryId = item.ProcurementSubcategoryId
                    });
                }
            }

            _context.CompanyInfoProcurementSubCategory.AddRange(SubCs);
        }

        /// <summary>
        /// saving the personnel details to the db
        /// </summary>
        /// <param name="personnelDetailInputs">list of custom personnel detail model</param>
        /// <param name="id">id of the company</param>
        private void AddPersonnelDetailsToDB(List<PersonnelDetailInput> personnelDetailInputs, int id)
        {
            // string companyCode = "_" + id + "_";
            var pd = new List<PersonnelDetails>();

            foreach (var pdInput in personnelDetailInputs)
            {
                var CertFileName = pdInput.CertFile.FileName;
                var CvFileName = pdInput.CVFile.FileName;
                var PassPortFileName = pdInput.PassportFile.FileName;

                //get path for storing files
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Personneldocs", id.ToString());

                //checks if path exists, if not create it.
                Directory.CreateDirectory(uploads);

                //create file paths for docs
                var CertfilePath = Path.Combine(uploads, CertFileName);
                var CvfilePath = Path.Combine(uploads, CvFileName);
                var PassPortfilePath = Path.Combine(uploads, PassPortFileName);

                //save files to folder
                pdInput.CertFile.CopyTo(new FileStream(CertfilePath, FileMode.Create));
                pdInput.CVFile.CopyTo(new FileStream(CvfilePath, FileMode.Create));
                pdInput.PassportFile.CopyTo(new FileStream(PassPortfilePath, FileMode.Create));

                pd.Add(new PersonnelDetails
                {
                    Certificate = CertFileName,
                    CV = CvFileName,
                    Name = pdInput.Name,
                    Passport = PassPortFileName,
                    Qualification = pdInput.Qualification,
                    CompanyInfoId = id
                });
            }

            _context.PersonnelDetails.AddRange(pd);
        }

        private async Task<string> SignUpUserAsync(CompanyInfo cp)
        {
            var user = new VendorUser
            {
                UserName = cp.Email,
                Email = cp.Email,
                CreationDate = DateTime.Now,
                PhoneNumber = cp.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, cp.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Company created a new account with password.");

                return "userId=" + user.Id;
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return "error";
        }

        public void AddPrequalificationJob(int companyID)
        {
            if (_context.Workflows.Where(m => m.WorkflowType.Name == "procurement").Any())
            {
                var wkflw = _context.Workflows.Where(m => m.WorkflowType.Name == "procurement").OrderBy(x => x.Step).First();

                _context.PrequalificationJobs.Add(new PrequalificationJob
                {
                    CompanyInfoId = companyID,
                    CreationDate = DateTime.Now,
                    WorkFlowStep = 1,
                });
            }
            else
            {
                _context.PrequalificationJobs.Add(new PrequalificationJob
                {
                    CompanyInfoId = companyID,
                    CreationDate = DateTime.Now,
                    WorkFlowStep = 0,
                });
            }
        }

        /// <summary>
        /// save company profile to new jobs
        /// </summary>
        /// <param name="company"></param>
        public void AddCompanyToJobs(int companyID)
        {
            //get first workflowstep

            //set job workflow step to 0 if no workflow exists

            var wrkFlow = _context.Workflows.Where(m => m.WorkflowType.Name == "procurement").MinBy(x => x.Step).FirstOrDefault();

            //if (wrkFlow.ToPersonOrAssign)
            //{
            //    _context.PrequalificationJobs.Add(new PrequalificationJob
            //    {
            //        CompanyInfoId = companyID,
            //        CreationDate = DateTime.Now,
            //        StaffId = wrkFlow.StaffId,
            //        WorkFlowStep = wrkFlow.Step,
            //    });
            //}
            //else
            //{
            //    _context.PrequalificationJobs.Add(new PrequalificationJob
            //    {
            //        CompanyInfoId = companyID,
            //        CreationDate = DateTime.Now,
            //        StaffId = wrkFlow.StaffId,
            //        WorkFlowStep = 0,
            //    });
            //}
        }
    }
}