using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff
{
    public class CoCModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public CoCModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProcurementCategory ProcurementCategory { get; set; }
        public List<ProcurementCategory>  ProcurementCategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
           ProcurementCategories = _context.ProcurementCategories.ToList();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            var check = _context.ProcurementCategories.FirstOrDefault(x => x.Name == ProcurementCategory.Name);

            if (check!=null)
            {
                Error = "Category with this name already exist.";
                return;
            }
            _context.ProcurementCategories.Add(ProcurementCategory);
            _context.SaveChanges();
            ProcurementCategories = _context.ProcurementCategories.ToList();

            Message = "Saved Successfully";
        }
    }
}