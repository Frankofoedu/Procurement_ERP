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
    public class PurchaseItemModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public PurchaseItemModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProcurementItem PItem { get; set; }
        public List<ProcurementItem> PItems { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            PItems = _context.ProcurementItems.Include(m=>m.Item).Where(m=>m.ProcurementGroupId==null).ToList();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
            } else if (PItem.ItemId == null)
            {
                Error = "Please click the search button to validate the item code.";
            }
            else
            {
                var check = _context.Items.FirstOrDefault(x => x.Id == PItem.ItemId);
                if (check != null)
                {
                    Error = "Please click the search button to validate the item code.";
                }
                else
                {
                    PItem.DateAdded = DateTime.UtcNow;

                    _context.ProcurementItems.Add(PItem);
                    _context.SaveChanges();

                    Message = "Saved Successfully";
                }
            }

            PItems = _context.ProcurementItems.Include(m => m.Item).Where(m => m.ProcurementGroupId == null).ToList();
        }
    }
}