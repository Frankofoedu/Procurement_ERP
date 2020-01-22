using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcAuthorization
{
    [Authorize]
    public class DetailRequisitionModel : PageModel
    {

        private readonly ProcurementDBContext _context;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly IItemGridViewModelService _itemGridViewModelService;
        private readonly IRequisitionService _requisitionService;
        public DetailRequisitionModel(ProcurementDBContext context, BSSLSYS_ITF_DEMOContext bsslContext, IItemGridViewModelService itemGridViewModelService, IRequisitionService requisitionService)
        {
            _context = context;
            _bsslContext = bsslContext;
            _itemGridViewModelService = itemGridViewModelService;
            _requisitionService = requisitionService;
        }
        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public Requisition Requisition { get; set; }
        [BindProperty]
        public List<ItemGridViewModel> ItemGridViewModels { get; set; }
        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

           await LoadData();
 

            return Page();
        }


        async Task LoadData()
        {
            Requisition = await _context.Requisitions.FirstOrDefaultAsync(x => x.Id == Id);

            //   ItemGridViewModels = Requisition.RequisitionItems.Select(x=> new ItemGridViewModel { Attachment = x.Attachment, RequisitionItem = x });

            if (Requisition == null)
            {
                Error = "No requisition Found";
                return;
            }
            ItemGridViewModels = await _itemGridViewModelService.GetItemsInRequisition(Id);


            WfVm = await _requisitionService.GetCurrentWorkFlowOFRequisition(Requisition);

            //checks if current logged in user is assigned staff. if not assigned, dont show workflow partial view
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;



            if (userId != WfVm.AssignedStaffCode)
            {
                WfVm = null;

                return;
            }

            WfVm.AssignedStaffCode = null;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var staffCode = WfVm.AssignedStaffCode;
            var newStage = WfVm.WorkFlowId;
            var remark = WfVm.Remark;

           await _requisitionService.SendRequisitionToNextStageAsync(Id, staffCode, newStage, remark);
            //Message = "Requisition Sent!";


            return RedirectToPage("Allrequisition");
        }

    }
}