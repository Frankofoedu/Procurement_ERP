using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement
{
    [Authorize]
    public class ProcurementJobsModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IProcurementService procurementService;

        private readonly UserManager<User> _userManager;

        public ProcurementJobsModel(ProcurementDBContext context, IProcurementService _procurementService, UserManager<User> userManager)
        {
            _context = context;
            procurementService = _procurementService;
            _userManager = userManager;
        }

        public string Message { get; set; }
        public string Error { get; set; }
        public IEnumerable<IGrouping<int?, ProcurementJobViewModel>> ProcurementJobsGroups { get; set; } = new List<IGrouping<int?, ProcurementJobViewModel>>();


        public async Task OnGetAsync()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var procJobs = await procurementService.GetProcurementRequisitionsJobsAssignedToLoggedInUser(user.Id);

                if (procJobs.Count > 0 )
                {
                    ProcurementJobsGroups = procJobs.GroupBy(x => x.WorkflowAction);
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