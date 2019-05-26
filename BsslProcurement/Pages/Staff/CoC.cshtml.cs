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
        public ContractCategory ContractCategory { get; set; }
        public List<ContractCategory> ContractCategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            ContractCategories = _context.ContractCategories.ToList();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            var check = _context.ContractCategories.FirstOrDefault(x => x.CategoryName == ContractCategory.CategoryName);

            if (check!=null)
            {
                Error = "Category with this name already exist.";
                return;
            }
            _context.ContractCategories.Add(ContractCategory);
            _context.SaveChanges();
            ContractCategories = _context.ContractCategories.ToList();

            Message = "Saved Successfully";
        }
    }
}