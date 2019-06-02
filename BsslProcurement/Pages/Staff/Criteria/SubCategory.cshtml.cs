using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.Criteria
{
    public class SubCategoryModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public SubCategoryModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubCategoryCriteria SubCategoryCriteria { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public IActionResult OnGet(int? delId)
        {

            //if (delId != null)
            //{
            //    var delItem = _context.ProcurementCriteria.FirstOrDefault(x => x.Id == delId);

            //    if (delItem != null)
            //    {
            //        _context.ProcurementCriteria.Remove(delItem);
            //        _context.SaveChanges();
            //        Message = "Deleted Successfully";
            //    }
            //}

            return Page();
        }


        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            if (SubCategoryCriteria.ProcurementSubcategoryId == null)
            {
                Error = "Enter Sub-category code and click the search button to validate it.";
                return;
            }

            var check = _context.SubCategoryCriterias.FirstOrDefault(x => x.ProcurementSubcategoryId ==
                SubCategoryCriteria.ProcurementSubcategoryId && x.CriteriaDescription == SubCategoryCriteria.CriteriaDescription);

            if (check != null)
            {
                Error = "Criteria with this Description already exist for this Sub-Category.";
                return;
            }

            _context.SubCategoryCriterias.Add(SubCategoryCriteria);
            _context.SaveChanges();

            Message = "Added Successfully";
            return;
        }

    }
}