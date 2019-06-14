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

       // public IList<PrequalificationWorkflow> Steps { get; set; }
        public int Counter { get; set; }

        [BindProperty]
        public List<PrequalificationJob> PreQJobsGroups { get; set; }

        public async Task OnGetAsync()
        {


            //get all the steps
            try
            {
                Counter = _context.PrequalificationWorkflows.Max(x => x.Step);
            }
            catch (ArgumentNullException)
            {
                Counter = 0;
                throw;
            }

            PrequalificationJob = await _context.PrequalificationJobs.Where(x => x.Done == false)
                .Include(p => p.CompanyInfo)
                .Include(p => p.Staff).ToListAsync();



        }
        public void OnPost()
        {
          //  return ;
        }

        }
    }
