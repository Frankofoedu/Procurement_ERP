using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff
{
    public class CCSGModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public CCSGModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProcurementSubcategory ProcurementSubcategory { get; set; }
        [BindProperty]
        public int categoryId { get; set; }
        public ProcurementCategory ProcurementCategory { get; set; }
        public List<ProcurementSubcategory> ProcurementSubcategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public IActionResult OnGet(int? id, int? delId)
        {
            if (id==null)
            { return LocalRedirect("~/Staff/CoC"); }

            if (delId != null)
            {
                var subToDel = _context.ProcurementSubcategories.FirstOrDefault(k => k.Id == delId);
                if (subToDel != null)
                {
                    _context.ProcurementSubcategories.Remove(subToDel);
                    _context.SaveChanges();
                }
            }


            var category = _context.ProcurementCategories.Include(y => y.ProcurementSubcategories).FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return LocalRedirect("~/Staff/CoC");
            }

            ProcurementSubcategories = category.ProcurementSubcategories.ToList();

            ProcurementCategory = category;
            categoryId = id.Value;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return Page();
            }

            var check = _context.ProcurementSubcategories.FirstOrDefault(x => x.ProcurementContractCategoryId == 
                categoryId && x.Name == ProcurementSubcategory.Name);

            if (check != null)
            {
                Error = "Subcategory with this name already exist.";
                return Page();
            }

            ProcurementSubcategory.ProcurementContractCategoryId = categoryId;

            _context.ProcurementSubcategories.Add(ProcurementSubcategory);
            _context.SaveChanges();

            var category = _context.ProcurementCategories.Include(y => y.ProcurementSubcategories).FirstOrDefault(x => x.Id == categoryId);

            if (category == null)
            {
                return LocalRedirect("~/Staff/CoC");
            }

            ProcurementSubcategories = category.ProcurementSubcategories.ToList();

            ProcurementCategory = category;
            categoryId = category.Id;
            ProcurementSubcategory = new ProcurementSubcategory();

            Message = "Saved Successfully";
            return Page();
        }
    }
}