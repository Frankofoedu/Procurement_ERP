using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BsslProcurement.Pages.Vendor
{
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


        public readonly IHostingEnvironment _hostingEnvironment;
        public PrequalificationModel(ProcurementDBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


       

        [BindProperty]
        public List<DocsModel> DocsList { get; set; }

        [BindProperty]
        public CompanyInfo CompanyInfo { get; set; }

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }


        public class PersonnelDetailInput
        {
            public string Name { get; set; }
            public string Qualification { get; set; }
            public IFormFile CVFile { get; set; }
            public IFormFile CertFile { get; set; }
            public IFormFile PassportFile { get; set; }
        }

        [BindProperty]
        public List<PersonnelDetailInput> PersonnelDetailIntputs { get; set; }//add method to convert iformfiles to file paths for personnel details table
        [BindProperty]
        public List<EquipmentDetails> EquipmentDetails { get; set; }
        [BindProperty]
        public List<ExperienceRecord> ExperienceRecords { get; set; }


        public string Message { get; set; }
        public string Error { get; set; }
        public int CategoryCount { get; set; }

        [BindProperty]
        public List<int> subCatsId { get; set; }
        public void OnGet()
        {
           // gets the number of categories from the setup page
            CategoryCount = _context.PrequalificationPolicies.FirstOrDefault().NoOfCategory;

            GetCategories();

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

            //saves company to db
            _context.CompanyInfo.Add(CompanyInfo);


            //saves list of submitted criterias to db
           _context.SubmittedCriteria.AddRange(GetSubmittedCriterias(CompanyInfo.Id));

            _context.SaveChanges();

            return null;


        }
        List<ProcurementSubcategory> GetSubCategories(List<int> ids)
        {
            var subCats = _context.ProcurementSubcategories.Where(p => ids.Contains(p.Id)).ToList();

            if (subCats.Any())
            {
                return subCats;
            }

            return null;
        }


        //update docs model filenames
        void GetImageFileName(List<DocsModel> formFiles)
        {
            if (formFiles.Any())
            {
                foreach (var doc in formFiles)
                {
                    //check if it requires a document upload
                    if (doc.isDoc)
                    {
                        var fileName = doc.Image.FileName;
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "docs");
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
        List<SubmittedCriteria> GetSubmittedCriterias(int id)
        {
            GetImageFileName(DocsList);

            var SubmittedCriterias = new List<SubmittedCriteria>();

            foreach ( var docs in DocsList)
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
        void AddExperienceToDB(int id, List<ExperienceRecord>  experienceRecords)
        {
            experienceRecords.ForEach(x => x.CompanyInfoId = id);

            _context.ExperienceRecord.AddRange(experienceRecords);
        }


        /// <summary>
        /// method for saving equipments details using company id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="experienceRecords"></param>
        void AddEquipmentsToDB(int id, List<EquipmentDetails> equipmentDetails)
        {
            equipmentDetails.ForEach(x => x.CompanyInfoId = id);

            _context.EquipmentDetails.AddRange(equipmentDetails);
        }


        void AddPersonnelDetailsToDB(List<PersonnelDetailInput> personnelDetailInputs, int id)
        {
            string companyCode = "_" + id + "_";
            var pd = new List<PersonnelDetails>();

            foreach (var pdInput in personnelDetailInputs)
            {
                var CertFileName = pdInput.CertFile.FileName;
                var CvFileName = pdInput.CVFile.FileName;
                var PassPortFileName = pdInput.PassportFile.FileName;

                //get path for storing files
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "docs");
                 

                //create file paths for docs
                var CertfilePath = Path.Combine(uploads, companyCode + CertFileName);
                var CvfilePath = Path.Combine(uploads, companyCode + CvFileName);
                var PassPortfilePath = Path.Combine(uploads, companyCode + PassPortFileName);

                //save files to folder
                pdInput.CertFile.CopyTo(new FileStream(CertfilePath, FileMode.Create));
                pdInput.CVFile.CopyTo(new FileStream(CvfilePath, FileMode.Create));
                pdInput.PassportFile.CopyTo(new FileStream(PassPortfilePath, FileMode.Create));

                pd.Add(new PersonnelDetails { Certificate = CertFileName, CV = CvFileName,
                                            Name = pdInput.Name, Passport = PassPortFileName,
                                            Qualification = pdInput.Qualification });

            }


        }


    }
}