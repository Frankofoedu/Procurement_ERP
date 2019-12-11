using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.BidPreparation
{
    public class ErfxViewModel
    {
        public string BidType { get; set; }
        public DateTime? BidStartDate { get; set; }
        public DateTime? BidEndDate { get; set; }
        public DateTime? TechnicalBidStartDate { get; set; }
        public DateTime? TechnicalBidEndDate { get; set; }
        public DateTime? FinancialBidStartDate { get; set; }
        public DateTime? FinancialBidEndDate { get; set; }

        public DateTime? ErfxDate { get; set; }
        public string ProjectTitle { get; set; }
        public string ERFXNum { get; set; }
    }

    [Authorize]
    public class ERFXModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        public ERFXModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ErfxViewModel ERFXViewModel { get; set; }
        public Requisition requisition { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public async Task OnGetAsync(int? reqId)
        {
            if (reqId == null)
            {
                Error = "No Requisition was Selected.";
                return;
            }

            requisition = await _context.Requisitions.Include(m => m.ERFXSetup).FirstOrDefaultAsync(n => n.Id == reqId.Value);

            if (requisition == null)
            {
                Error = "No Requisition was Selected.";
                return;
            }

                ERFXViewModel = new ErfxViewModel();

            if (requisition.ERFXSetup == null)
            {
                ERFXViewModel.ERFXNum = reqId.Value.ToString("0000");
                ERFXViewModel.ErfxDate = DateTime.Now;
                ERFXViewModel.ProjectTitle = requisition.Description;
            }
            else
            {
                var erfx = requisition.ERFXSetup;

                ERFXViewModel.ERFXNum = erfx.ErfxNum;
                ERFXViewModel.ErfxDate = erfx.ErfxDate;
                ERFXViewModel.ProjectTitle = erfx.ProjectTitle;
            }
        }
        public void OnPostAsync(int? reqId)
        {
            var er = ERFXViewModel;
        }
    }
}