using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement
{

    [Authorize(AuthenticationSchemes = "Vendors")]
    public class BidProcessModel : PageModel
    {
        [BindProperty]
        public ViewModels.InvitePartialInputViewModel InviteInput { get; set; }

        [BindProperty]
        public ViewModels.DeclarationPartialInputViewModel DeclarationInput { get; set; }

        [BindProperty]
        public ViewModels.GeneralInformationPartialViewModel GeneralInput { get; set; }

        private readonly ProcurementDBContext _context;

        public string Message { get; set; }
        public string Error { get; set; }

        public BidProcessModel(ProcurementDBContext context)
        {
            _context = context;

        }

        public async Task OnGetAsync(int? reqId)
        {
            if (reqId == null)
            {
                Error = "No Requisition was Selected.";
                return;
            }

            var requisition = await _context.Requisitions.Include(m => m.ERFXSetup).ThenInclude(m => m.FinancialERFXSetup).Include(m => m.ERFXSetup)
                .ThenInclude(m => m.TechnicalERFXSetup).Include(m => m.RequisitionItems).FirstOrDefaultAsync(n => n.Id == reqId.Value);

            if (requisition == null)
            {
                Error = "Requisition was NOT found.";
                return;
            }

            InviteInput = new ViewModels.InvitePartialInputViewModel();
            DeclarationInput = new ViewModels.DeclarationPartialInputViewModel();
            GeneralInput = new ViewModels.GeneralInformationPartialViewModel();

            
        }

        public void OnPost()
        {
            var m = DeclarationInput.Name;
        }
    }
}