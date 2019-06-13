using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DcProcurement;

namespace BsslProcurement.Pages.Staff.JobViews
{
    public class PreqJobsModel : PageModel
    {
        private readonly DcProcurement.ProcurementDBContext _context;

        public PreqJobsModel(DcProcurement.ProcurementDBContext context)
        {
            _context = context;
        }

        public IList<PrequalificationJob> PrequalificationJob { get;set; }



        public async Task OnGetAsync()
        {
            PrequalificationJob = await _context.PrequalificationJobs
                .Include(p => p.CompanyInfo)
                .Include(p => p.Staff).ToListAsync();
        }
    }
}
