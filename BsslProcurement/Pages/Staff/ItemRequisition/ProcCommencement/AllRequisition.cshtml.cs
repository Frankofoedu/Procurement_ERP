using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Filters.Attributes;
using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Contexts;
using DcProcurement.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcCommencement
{
    [Authorize]
    [NoDiscovery]
    [DisplayName("Approved Requisitions")]
    public class AllRequisitionModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IProcurementService _procurementService;
        private readonly IJobService _jobService;

        private readonly UserManager<User> _userManager;
        public AllRequisitionModel(ProcurementDBContext context, IProcurementService procurementService, IJobService jobService, UserManager<User> userManager)
        {
            _context = context;
            _procurementService = procurementService;
            _jobService = jobService;
            _userManager = userManager;
        }
        public string Message { get; set; }
        public string Error { get; set; }

        public List<Requisition> Requisitions { get; set; }

        public List<RequisitionJob> LastRequisitionJobs { get; set; }

        public async Task OnGetAsync()
        {
            Requisitions = await _procurementService.GetApprovedRequisitions();

            LastRequisitionJobs = await _jobService.GetApprovalJobsForRequisitionsAsync(Requisitions);
            
        }

        //private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}