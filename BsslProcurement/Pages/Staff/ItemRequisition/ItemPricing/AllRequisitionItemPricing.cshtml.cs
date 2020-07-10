using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Contexts;
using DcProcurement.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ItemPricing
{
    [Authorize]
    public class AllRequisitionItemPricingModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IProcurementService _procurementService;
        private readonly UserManager<User> _userManager;
        private readonly IJobService _jobService;

        public AllRequisitionItemPricingModel(ProcurementDBContext context, IProcurementService procurementService, 
            IJobService jobService, UserManager<User> userManager)
        {
            _context = context;
            _procurementService = procurementService;
            _userManager = userManager;
            _jobService = jobService;

        }


        public string Message { get; set; }
        public string Error { get; set; }

        public List<RequisitionJob> LastRequisitionJobs { get; set; }
        public List<Requisition> Requisitions { get; set; } = new List<Requisition>();

        [BindProperty]
        public string PrType { get; set; }

        public async Task OnGetAsync()
        {
            var user = await GetCurrentUserAsync();

            //get all requisitions that havent been priced
            Requisitions = await _procurementService.GetRequisitionsForPricingAssignedToUser(user.Id);

            LastRequisitionJobs = await _jobService.GetApprovalJobsForRequisitionsAsync(Requisitions);
        }

        public async Task OnPostAsync()
        {
            var user = await GetCurrentUserAsync();

            //get all requisitions that havent been priced
            Requisitions = await _procurementService.GetRequisitionsForPricingAssignedToUser(user.Id);

            if (PrType != "all")
            {
                Requisitions = Requisitions.Where(m => m.ProcurementType == PrType).ToList();
            }
            LastRequisitionJobs = await _jobService.GetApprovalJobsForRequisitionsAsync(Requisitions);
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}