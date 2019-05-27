using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff
{
    public class ItemsModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public ItemsModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProcurementItem ProcurementItem { get; set; }
        public List<ProcurementItem> ProcurementItems { get; set; }
        public List<ContractSubcategory> ContractSubcategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            ProcurementItems = _context.ProcurementItems.ToList();

            var subCategories = _context.ContractSubcategories.AsNoTracking().ToList();
            foreach (var item in subCategories)
            { item.ContractCategory = null; }

            ContractSubcategories = subCategories;

            ViewData["GroupItems"] = new SelectList(_context.ContractCategories, "Id", "CategoryName");
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            ProcurementItem.DateAdded = DateTime.UtcNow;

            _context.ProcurementItems.Add(ProcurementItem);
            _context.SaveChanges();

            ProcurementItems = _context.ProcurementItems.ToList();

            var subCategories = _context.ContractSubcategories.AsNoTracking().ToList();
            foreach (var item in subCategories)
            { item.ContractCategory = null; }

            ContractSubcategories = subCategories;

            ViewData["GroupItems"] = new SelectList(_context.ContractCategories, "Id", "CategoryName");

            Message = "Saved Successfully";
        }
    }
}