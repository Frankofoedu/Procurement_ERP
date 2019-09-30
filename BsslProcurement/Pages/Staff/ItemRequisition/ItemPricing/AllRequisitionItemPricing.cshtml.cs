using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ItemPricing
{
    public class AllRequisitionItemPricingModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        public AllRequisitionItemPricingModel(ProcurementDBContext context)
        {
            _context = context;
            
        }


        public string Message { get; set; }
        public string Error { get; set; }



        [BindProperty]
        public List<Requisition> Requisitions { get; set; }
        public async Task OnGetAsync()
        {
            //get all requisitions that havent been priced
            Requisitions = await _context.Requisitions.Where(r => r.isPriced == false).Include(x => x.RequisitionItems).ToListAsync();
            
        }
    }
}