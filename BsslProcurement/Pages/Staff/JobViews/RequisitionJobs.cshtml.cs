using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Filters.Attributes;
using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement
{
    [Authorize]
    [DisplayName("Requisition Tasks")]
    [NoDiscovery]
    public class RequisitionJobsModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IRequisitionService requisitionService;

        private readonly UserManager<User> _userManager;

        public RequisitionJobsModel(ProcurementDBContext context, IRequisitionService _requisitionService, UserManager<User> userManager)
        {
            _context = context;
            requisitionService = _requisitionService;
            _userManager = userManager;
        }

        public string Message { get; set; }
        public string Error { get; set; }
        public List<RequisitionJob> RequisitionJobs { get; set; }
        public List<RequisitionJob> PreviousRequisitionJobs { get; set; }
        public List<SelectListItem> RequisitionWorkFlows { get; set; }

        [BindProperty]
        public int WorkflowId { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                RequisitionJobs = (await requisitionService.GetRequisitionsJobsAssignedToLoggedInUser(user.Id)).OrderBy(m=>m.Workflow.WorkflowActionId).ToList();
                PreviousRequisitionJobs = new List<RequisitionJob>();
                foreach (var item in RequisitionJobs)
                {
                    var prj = await _context.RequisitionJobs.Include(n=>n.Staff).Where(m => m.RequisitionId == item.RequisitionId &&
                        m.JobStatus == Enums.JobState.Done).OrderByDescending(l => l.Id).FirstOrDefaultAsync();

                    if (prj != null) { PreviousRequisitionJobs.Add(prj); }
                    else { PreviousRequisitionJobs.Add(RequisitionJob.Empty()); }
                }

                RequisitionWorkFlows = (await requisitionService.GetRequisitionWorkflows()).Select(
                    m=> new SelectListItem() { 
                        Value = m.Id.ToString(), 
                        Text=m.WorkflowAction.Name
                    }).ToList();
            }
            catch (Exception ex)
            {

                Error = "Error has occured. Please contact Admin." + Environment.NewLine + ex.Message;
            }
        }
        public async Task OnPostAsync()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                RequisitionJobs = (await requisitionService.GetRequisitionsJobsAssignedToLoggedInUser(user.Id)).OrderBy(m => m.Workflow.WorkflowActionId).ToList();
                RequisitionWorkFlows = (await requisitionService.GetRequisitionWorkflows()).Select(
                    m => new SelectListItem()
                    {
                        Value = m.Id.ToString(),
                        Text = m.WorkflowAction.Name
                    }).ToList();

                if (WorkflowId > 0)
                {
                    RequisitionJobs = RequisitionJobs.Where(m => m.WorkFlowId == WorkflowId).ToList();
                }

                PreviousRequisitionJobs = new List<RequisitionJob>();
                foreach (var item in RequisitionJobs)
                {
                    var prj = await _context.RequisitionJobs.Include(n => n.Staff).Where(m => m.RequisitionId == item.RequisitionId &&
                          m.JobStatus == Enums.JobState.Done).OrderByDescending(l => l.Id).FirstOrDefaultAsync();

                    if (prj != null) { PreviousRequisitionJobs.Add(prj); }
                    else { PreviousRequisitionJobs.Add(RequisitionJob.Empty()); }
                }
            }
            catch (Exception ex)
            {
                Error = "Error has occured. Please contact Admin." + Environment.NewLine + ex.Message;
            }
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}