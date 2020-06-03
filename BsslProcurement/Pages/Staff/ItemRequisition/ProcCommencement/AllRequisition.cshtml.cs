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

        private readonly UserManager<User> _userManager;
        public AllRequisitionModel(ProcurementDBContext context, IProcurementService procurementService, UserManager<User> userManager)
        {
            _context = context;
            _procurementService = procurementService;
            _userManager = userManager;
        }
        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Requisition> Requisitions { get; set; }

        public List<RequisitionJob> LastRequisitionJobs { get; set; }

        public async Task OnGetAsync()
        {

            Requisitions = await _procurementService.GetApprovedRequisitions();

            LastRequisitionJobs = new List<RequisitionJob>();
            foreach (var item in Requisitions)
            {
                var prj = await _context.RequisitionJobs.Include(n => n.Staff).Where(m => m.RequisitionId == item.Id)
                    .OrderByDescending(l => l.Id).FirstOrDefaultAsync();

                if (prj != null) { LastRequisitionJobs.Add(prj); }
                else { LastRequisitionJobs.Add(RequisitionJob.Empty()); }
            }
        }

        //private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}