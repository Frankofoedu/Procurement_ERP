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
    }
}