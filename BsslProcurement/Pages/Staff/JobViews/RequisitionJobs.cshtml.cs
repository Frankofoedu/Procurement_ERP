using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement
{
    [Authorize]
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
        [BindProperty]
        public IEnumerable<IGrouping<int?,RequisitionJob>> RequisitionJobs { get; set; }


        public async Task OnGetAsync()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var t = await requisitionService.GetRequisitionsJobsAssignedToLoggedInUser(user.Id);
                if (t.Count > 0)
                {

                    RequisitionJobs = t.GroupBy(x => x.Workflow.WorkflowTypeId);
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