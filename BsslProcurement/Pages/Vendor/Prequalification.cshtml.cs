using BsslProcurement.Const;
using BsslProcurement.UtilityMethods;
using DcProcurement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Pages.Vendor
{
    //TODO: Cheeck for errors if any returned entity doesnt and display error meesage
    public class PersonnelDetailInput
    {
        public string Name { get; set; }
        public string Qualification { get; set; }
        public IFormFile CVFile { get; set; }
        public IFormFile CertFile { get; set; }
        public IFormFile PassportFile { get; set; }
    }
    public class DocsModel
    {
        public int Id { get; set; }
        public bool isDoc { get; set; }
        public IFormFile Image { get; set; }
        public string FileName { get; set; }
        public string Value { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        [Required]       
        public string Password { get; set; }
    }
    public class PrequalificationModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<PrequalificationModel> _logger;
        private readonly UserManager<User> _userManager;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public PrequalificationModel(ProcurementDBContext context, IWebHostEnvironment hostingEnvironment,                           UserManager<User> userManager, SignInManager<User> signInManager, ILogger<PrequalificationModel> logger)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [BindProperty]
        public LoginModel LoginModel { get; set; }
        [BindProperty]
        public IFormFile CompanyProfile { get; set; }
        [BindProperty]
        public IFormFile CertificateOfIncorporation { get; set; }
        [BindProperty]
        public IFormFile TaxCertificate { get; set; }
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

        

        public string Message { get; set; }
        public string Error { get; set; }
        public int? CategoryCount { get; set; }

        //[BindProperty]
        //public List<int> subCatsId { get; set; }

        public IActionResult OnGet()
        {
            // gets the number of categories from the setup page
            LoadData();
            return Page();
        }

        private void LoadData()
        {
            var setup = _context.PrequalificationPolicies.FirstOrDefault();
            if (setup == null)
            {
                //Error = "Prequalification has not yet started";
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
                LoadData();
                return Page();
            }


            //saves company to db
            _context.CompanyInfo.Add(CompanyInfo);

            await _context.SaveChangesAsync();

            //saves list of submitted criterias to db
            _context.SubmittedCriteria.AddRange(GetSubmittedCriterias(CompanyInfo.Id));

            AddEquipmentsToDB(CompanyInfo.Id, EquipmentDetails);

            AddExperienceToDB(CompanyInfo.Id, ExperienceRecords);

            AddSelectedSubcategoriesToDB(CompanyInfo.Id, SelectedSubcategories);

            AddPersonnelDetailsToDB(PersonnelDetailIntputs, CompanyInfo.Id);

           await AddPrequalificationJob(CompanyInfo.Id);
            
            //saves tax, incorporation cert docs
            await SaveAttachements();

            await _context.SaveChangesAsync();

            var rtn = await SignUpUserAsync(CompanyInfo);

            if (rtn == "error")
            {
                Error = "An Error occured!";
                return Page();
            }


            CompanyInfo.VendorId = rtn.Replace("userId=", "", StringComparison.Ordinal);

            await _context.SaveChangesAsync();

            Message = "Your Information has been successfully uploaded";

            return null; //change to redirect location
        }

        private async Task SaveAttachements()
        {
            //upload company profile
            var compProfile = await FileUpload.GetFilePathsFromFileAsync(CompanyProfile, _hostingEnvironment, ConstantVals.AttachmentFolder);

            //upload company inc
            var compInc = await FileUpload.GetFilePathsFromFileAsync(CertificateOfIncorporation, _hostingEnvironment, ConstantVals.AttachmentFolder);


            //upload company tax
            var compTax = await FileUpload.GetFilePathsFromFileAsync(TaxCertificate, _hostingEnvironment, ConstantVals.AttachmentFolder);


            //save in db
            CompanyInfo.CompanyProfile = compProfile;
            CompanyInfo.CertificateOfIncorp = compInc;
            CompanyInfo.TaxRegistration = compTax;
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

        //private List<ProcurementSubcategory> GetSubCategories(List<int> ids)
        //{
        //    var subCats = _context.ProcurementSubcategories.Where(p => ids.Contains(p.Id)).ToList();

        //    if (subCats.Any())
        //    {
        //        return subCats;
        //    }

        //    return null;
        //}

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
        private async Task AddPersonnelDetailsToDB(List<PersonnelDetailInput> personnelDetailInputs, int id)
        {
            // string companyCode = "_" + id + "_";
            var pd = new List<PersonnelDetails>();

            foreach (var pdInput in personnelDetailInputs)
            {
                var ctAttcTask =  FileUpload.GetFilePathsFromFileAsync(pdInput.CertFile, _hostingEnvironment, ConstantVals.AttachmentFolder);

                var cvAttcTask =  FileUpload.GetFilePathsFromFileAsync(pdInput.CVFile, _hostingEnvironment, ConstantVals.AttachmentFolder);

                var passAttcTask =  FileUpload.GetFilePathsFromFileAsync(pdInput.PassportFile, _hostingEnvironment, ConstantVals.AttachmentFolder);

                //wait for all file upload task to be finished
               await Task.WhenAll(ctAttcTask, cvAttcTask, passAttcTask);


                pd.Add(new PersonnelDetails
                {
                    Certificate = ctAttcTask.Result,
                    CV = cvAttcTask.Result,
                    Name = pdInput.Name,
                    Passport = passAttcTask.Result,
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
                UserName = LoginModel.Email,
                Email = LoginModel.Email,
                CreationDate = DateTime.Now,
                PhoneNumber = cp.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, LoginModel.Password);
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

        public async Task AddPrequalificationJob(int companyID)
        {
            //get first prequalification workflow
            var wrkf = await _context.Workflows.Where(m => m.WorkflowTypeId == Constants.PrequalificationWorkFlowId).Include(m=> m.Staffs).OrderBy(m=> m.Step).FirstOrDefaultAsync();

            if (wrkf != null)
            {
                var stf = wrkf.Staffs.FirstOrDefault();
                if (stf != null)
                {
                    var compJob = new PrequalificationJob(companyID, stf.StaffId, wrkf.Id, "Please review new company profile");

                    _context.PrequalificationJobs.Add(compJob);                    
                }
                
            }
           
        }

    }
}