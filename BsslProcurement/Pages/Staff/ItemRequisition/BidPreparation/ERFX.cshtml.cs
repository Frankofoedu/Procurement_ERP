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
        public int ReqId { get; set; }
        public Enums.BidTypes BidType { get; set; }
        public DateTime? TechnicalBidStartDate { get; set; }
        public DateTime? TechnicalBidEndDate { get; set; }
        public DateTime? FinancialBidStartDate { get; set; }
        public DateTime? FinancialBidEndDate { get; set; }
        public bool SameDate { get; set; }

        public DateTime? ErfxDate { get; set; }
        public string ProjectTitle { get; set; }
        public string ERFXNum { get; set; }
        public string PRQNum { get; set; }
        public DateTime? PRQDate { get; set; }
        public int ItemNum { get; set; }
        public string CategoryItem { get; set; }
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

        [BindProperty]
        public List<string> VendorEmails { get; set; }

        [BindProperty]
        public List<string> StaffEmails { get; set; }

        public List<VendorUser> VendorList { get; set; }

        public List<DcProcurement.Staff> StaffList { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public async Task OnGetAsync(int? reqId)
        {
            VendorList = new List<VendorUser>();

            if (reqId == null)
            {
                Error = "No Requisition was Selected.";
                return;
            }

            var requisition = await _context.Requisitions.Include(m => m.ERFXSetup).ThenInclude(m=> m.FinancialERFXSetup).Include(m=>m.ERFXSetup)
                .ThenInclude(m => m.TechnicalERFXSetup).Include(m=>m.RequisitionItems).FirstOrDefaultAsync(n => n.Id == reqId.Value);

            if (requisition == null)
            {
                Error = "No Requisition was Selected.";
                return;
            }

            VendorList = _context.Vendors.Include(m => m.CompanyInfo).ToList();
            StaffList = _context.Staffs.ToList();

            ERFXViewModel = new ErfxViewModel();
            ERFXViewModel.ReqId = reqId.Value;

            if (requisition.ERFXSetup == null)
            {
                ERFXViewModel.ERFXNum = reqId.Value.ToString("0000");
                ERFXViewModel.ErfxDate = DateTime.Now;
                ERFXViewModel.ProjectTitle = requisition.Description;
                ERFXViewModel.BidType = Enums.BidTypes.Both;
                ERFXViewModel.SameDate = true;
            }
            else
            {
                var erfx = requisition.ERFXSetup;

                ERFXViewModel.ERFXNum = erfx.ErfxNum;
                ERFXViewModel.ErfxDate = erfx.ErfxDate;
                ERFXViewModel.ProjectTitle = erfx.ProjectTitle;
                ERFXViewModel.BidType = erfx.BidType;

                
                if (erfx.BidType == Enums.BidTypes.Both)
                {
                    ERFXViewModel.TechnicalBidEndDate = erfx.TechnicalERFXSetup.BidEndDate;
                    ERFXViewModel.TechnicalBidStartDate = erfx.TechnicalERFXSetup.BidStartDate;
                    ERFXViewModel.FinancialBidEndDate = erfx.FinancialERFXSetup.BidEndDate;
                    ERFXViewModel.FinancialBidStartDate = erfx.FinancialERFXSetup.BidStartDate;

                    if ((erfx.FinancialERFXSetup.BidStartDate == erfx.TechnicalERFXSetup.BidStartDate) &&
                        (erfx.FinancialERFXSetup.BidEndDate == erfx.TechnicalERFXSetup.BidEndDate))
                    {
                        ERFXViewModel.SameDate = true;
                    }
                }
                else if (erfx.BidType == Enums.BidTypes.Technical)
                {
                    ERFXViewModel.TechnicalBidEndDate = erfx.TechnicalERFXSetup.BidEndDate;
                    ERFXViewModel.TechnicalBidStartDate = erfx.TechnicalERFXSetup.BidStartDate;
                }
                else if (erfx.BidType == Enums.BidTypes.Financial)
                {
                    ERFXViewModel.FinancialBidEndDate = erfx.FinancialERFXSetup.BidEndDate;
                    ERFXViewModel.FinancialBidStartDate = erfx.FinancialERFXSetup.BidStartDate;
                }

            }

            ERFXViewModel.PRQDate = requisition.Date;
            ERFXViewModel.PRQNum = requisition.PRNumber;
            ERFXViewModel.ItemNum = requisition.RequisitionItems.Count;


        }

        public async Task OnPostSaveAsync()
        {
            var requisition = await _context.Requisitions.Include(m => m.ERFXSetup).ThenInclude(m => m.FinancialERFXSetup).Include(m => m.ERFXSetup)
                .ThenInclude(m => m.TechnicalERFXSetup).Include(m => m.RequisitionItems).FirstOrDefaultAsync(n => n.Id == ERFXViewModel.ReqId);

            var erfxsetup = getErfxSetup(ERFXViewModel.ReqId, ERFXViewModel, false);

            if (requisition.ERFXSetup == null) { requisition.ERFXSetup = erfxsetup; }
            else {
                _context.Entry(requisition.ERFXSetup).CurrentValues.SetValues(erfxsetup);
                _context.Entry(requisition.ERFXSetup.FinancialERFXSetup).CurrentValues.SetValues(erfxsetup.FinancialERFXSetup);
                _context.Entry(requisition.ERFXSetup.TechnicalERFXSetup).CurrentValues.SetValues(erfxsetup.TechnicalERFXSetup);
            }

            await _context.SaveChangesAsync();

            Message = "Saved Successfully";
        }

        public async Task OnPostSubmitAsync()
        {
            var requisition = await _context.Requisitions.Include(m => m.ERFXSetup).ThenInclude(m => m.FinancialERFXSetup).Include(m => m.ERFXSetup)
                .ThenInclude(m => m.TechnicalERFXSetup).Include(m => m.RequisitionItems).FirstOrDefaultAsync(n => n.Id == ERFXViewModel.ReqId);

            var oldERFX = requisition.ERFXSetup;

            var erfxsetup = getErfxSetup(ERFXViewModel.ReqId, ERFXViewModel, true);

            if (oldERFX == null) { requisition.ERFXSetup = erfxsetup; }
            else {
                _context.Entry(requisition.ERFXSetup).CurrentValues.SetValues(erfxsetup);
                _context.Entry(requisition.ERFXSetup.FinancialERFXSetup).CurrentValues.SetValues(erfxsetup.FinancialERFXSetup);
                _context.Entry(requisition.ERFXSetup.TechnicalERFXSetup).CurrentValues.SetValues(erfxsetup.TechnicalERFXSetup);
            }

            await _context.SaveChangesAsync();

            Message = "Submitted Successfully";
        }

        public ERFXSetup getErfxSetup(int reqId, ErfxViewModel model, bool submit)
        {
            var erfx = new ERFXSetup(reqId);

            erfx.BidType = model.BidType;
            erfx.ErfxDate = model.ErfxDate;
            erfx.ProjectTitle = model.ProjectTitle;
            erfx.Submitted = submit;

            if (model.BidType == Enums.BidTypes.Technical) {
                getTechSetup(erfx, model);
            } 
            else if (model.BidType == Enums.BidTypes.Financial) {
                getFinSetup(erfx, model);
            } else {
                getTechSetup(erfx, model);
                getFinSetup(erfx, model);
            }

            return erfx;
        }

        public ERFXSetup getTechSetup(ERFXSetup setup, ErfxViewModel model)
        {
            var tech = new TechnicalERFXSetup();
            tech.BidEndDate = model.TechnicalBidEndDate;
            tech.BidStartDate = model.TechnicalBidStartDate;

            setup.TechnicalERFXSetup = tech;

            return setup;
        }

        public ERFXSetup getFinSetup(ERFXSetup setup, ErfxViewModel model)
        {
            var fin = new FinancialERFXSetup();
            fin.BidEndDate = model.FinancialBidEndDate;
            fin.BidStartDate = model.FinancialBidStartDate;

            setup.FinancialERFXSetup = fin;

            return setup;
        }
    }
}