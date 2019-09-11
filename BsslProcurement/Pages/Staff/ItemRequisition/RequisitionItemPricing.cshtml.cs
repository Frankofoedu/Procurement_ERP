using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DcProcurement;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    [Authorize]
    public class RequisitionItemPricingModel : PageModel
    {
        public readonly ProcurementDBContext context;


        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public DcProcurement.Requisition Requisition { get; set; }
        public RequisitionItemPricingModel(ProcurementDBContext _context)
        {
            context = _context;
        }
        public void OnGet(int id)
        {
            Requisition = context.Requisitions.Include(x => x.Attachments).Include(y => y.RequisitionItems).Where(k => k.Id== id).FirstOrDefault();
        }
    }
}