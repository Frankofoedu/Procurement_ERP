using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BsslProcurement.Filters.Attributes;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using DcProcurement.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    [Authorize]
    [NoDiscovery]
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
        public string ReturnUrl { get; set; }
        
        public string RequisitionJobRemarks { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Requisition Requisition { get; set; }
        [BindProperty]
        public List<ItemGridViewModel> ItemGridViewModels { get; set; }
        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }
        [BindProperty]
        public string QuarantineRemark { get; set; }
        [BindProperty]
        public string RejectionRemark { get; set; }
        public bool CanEdit { get; set; }

        public async Task OnGetAsync(bool isRejected, string returnUrl = null)
        {
            CanEdit = isRejected;
            returnUrl ??= Url.Content("~/");

            await LoadData();

            ReturnUrl = returnUrl;
        }


        async Task LoadData()
        {
            Requisition = await _context.Requisitions.FirstOrDefaultAsync(x => x.Id == Id);

            var RequisitionJobs = await _context.RequisitionJobs.Include(n=>n.Workflow).ThenInclude(n=>n.WorkflowAction)
                .Include(m=>m.Staff).Where(x => x.RequisitionId == Requisition.Id && x.JobStatus!= Enums.JobState.NotDone)
                .OrderByDescending(m=>m.Id).ToListAsync();

            foreach (var item in RequisitionJobs)
            {
                RequisitionJobRemarks += $"{item.Staff.Name.ToUpper()} -- ({item.Workflow.WorkflowAction.Name}) -- : {item.Remark} \n\n";
            }
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
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var staffCode = WfVm.AssignedStaffCode;
            var newStage = WfVm.WorkFlowId;
            var remark = WfVm.Remark;

            await _requisitionService.SendRequisitionToNextStageAsync(Id, staffCode, newStage, remark);
            //Message = "Requisition Sent!";


            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Deletes a Requisition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> OnPostQuarantineAsync(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");


            try
            {
                await _requisitionService.SendToQuarantine(Id, $"Quarantined - {QuarantineRemark}");

                Message = "Requisition Quarantined";
            }
            catch (Exception ex)
            {
                Error = "An error has occurred." + Environment.NewLine + ex.Message;
                await LoadData();
                return Page();
            }


            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Rejects a Requisition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> OnPostRejectAsync(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");


            try
            {
                await _requisitionService.RejectRequisition(Id, $"Rejected - {RejectionRemark}");

                Message = "Requisition Rejected";
            }
            catch (Exception ex)
            {
                Error = "An error has occurred." + Environment.NewLine + ex.Message;
                await LoadData();
                return Page();
            }


            return LocalRedirect(returnUrl);
        }
    }
}