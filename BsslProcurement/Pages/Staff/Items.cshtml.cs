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
        public Item Item { get; set; }
        public List<Item> dItems { get; set; }
        public List<ProcurementSubcategory> ContractSubcategories { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            dItems = _context.Items.Include(m=>m.ProcurementSubcategory).ToList();


            var subCategories = _context.ProcurementSubcategories.AsNoTracking().ToList();

            ContractSubcategories = new List<ProcurementSubcategory>();
            foreach (var item in subCategories)
            {
                ContractSubcategories.Add(new ProcurementSubcategory()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    ProcurementSubCategoryCode = item.ProcurementSubCategoryCode,
                    ProcurementCategoryId = item.ProcurementCategoryId,
                });
            }

            ViewData["GroupItems"] = new SelectList(_context.ProcurementCategories, "Id", "Name");
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            Item.ItemCode = Item.ItemCode.Trim();

            var check = _context.Items.FirstOrDefault(x => x.ItemCode == Item.ItemCode);
            if (check != null)
            {
                Error = "This CODE is already in USE!!!";
            }
            else
            {
                Item.DateAdded = DateTime.UtcNow;

                _context.Items.Add(Item);
                _context.SaveChanges();

                Message = "Saved Successfully";
            }

            dItems = _context.Items.Include(m => m.ProcurementSubcategory.ProcurementCategory).ToList();

            var subCategories = _context.ProcurementSubcategories.AsNoTracking().ToList();

            ContractSubcategories = new List<ProcurementSubcategory>();
            foreach (var item in subCategories)
            {
                ContractSubcategories.Add(new ProcurementSubcategory()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    ProcurementSubCategoryCode = item.ProcurementSubCategoryCode,
                    ProcurementCategoryId = item.ProcurementCategoryId,
                });
            }

            ViewData["GroupItems"] = new SelectList(_context.ProcurementCategories, "Id", "Name");

        }
    }
}