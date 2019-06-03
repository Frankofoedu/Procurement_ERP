using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BsslProcurement.Pages.Vendor
{
    public class PrequalificationModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public PrequalificationModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CompanyInfo CompanyInfo { get; set; }

        [BindProperty]
        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public List<ProcurementSubcategory> scat { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public int CategoryCount { get; set; }

        [BindProperty]
        public List<int> subCatsId { get; set; }
        public void OnGet()
        {
            //gets the number of categories from the setup page
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
                CategoryCount = _context.PrequalificationPolicies.FirstOrDefault().NoOfCategory;
                GetCategories();
                return Page();
            }



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

        

    }
}