using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.Criteria
{
    public class CategoryModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public CategoryModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryCriteria CategoryCriteria { get; set; }

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

            if (CategoryCriteria.ProcurementCategoryId == null)
            {
                Error = "Enter Category code and click the search button to validate it.";
                return;
            }

            var check = _context.CategoryCriterias.FirstOrDefault(x => x.ProcurementCategoryId ==
                CategoryCriteria.ProcurementCategoryId && x.CriteriaDescription == CategoryCriteria.CriteriaDescription);

            if (check != null)
            {
                Error = "Criteria with this Description already exist for this Category.";
                return;
            }

            _context.CategoryCriterias.Add(CategoryCriteria);
            _context.SaveChanges();

            Message = "Added Successfully";
            return;
        }

    }
}