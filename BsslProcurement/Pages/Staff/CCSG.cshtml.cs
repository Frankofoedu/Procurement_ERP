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
        public ContractSubcategory ContractSubcategory { get; set; }
        public List<ContractSubcategory> ContractSubcategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public IActionResult OnGet(int? id, int? delId)
        {
            if (id==null)
            { return LocalRedirect("~/Staff/CoC"); }

            if (delId != null)
            {
                var subToDel = _context.ContractSubcategories.FirstOrDefault(k => k.Id == delId);
                if (subToDel != null)
                { _context.ContractSubcategories.Remove(subToDel); }
            }

            _context.SaveChanges();

            var category = _context.ContractCategories.Include(y => y.ContractSubcategories).FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return LocalRedirect("~/Staff/CoC");
            }

            ContractSubcategories = category.ContractSubcategories.ToList();

            ContractSubcategory = new ContractSubcategory();
            ContractSubcategory.ContractCategory = category;
            ContractSubcategory.ContractCategoryId = id.Value;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return Page();
            }

            var check = _context.ContractSubcategories.FirstOrDefault(x => x.ContractCategoryId == 
                ContractSubcategory.ContractCategoryId && x.SubcategoryName == ContractSubcategory.SubcategoryName);

            if (check != null)
            {
                Error = "Subcategory with this name already exist.";
                return Page();
            }

            _context.ContractSubcategories.Add(ContractSubcategory);
            _context.SaveChanges();

            var category = _context.ContractCategories.Include(y => y.ContractSubcategories).FirstOrDefault(x => x.Id == ContractSubcategory.ContractCategoryId);

            if (category == null)
            {
                return LocalRedirect("~/Staff/CoC");
            }

            ContractSubcategories = category.ContractSubcategories.ToList();

            ContractSubcategory = new ContractSubcategory();
            ContractSubcategory.ContractCategory = category;
            ContractSubcategory.ContractCategoryId = category.Id;

            Message = "Saved Successfully";
            return Page();
        }
    }
}