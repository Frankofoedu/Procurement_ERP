using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DcProcurement;
using Microsoft.AspNetCore.Identity;

namespace BsslProcurement.Pages.Staff.JobViews
{
    public class InputModel : PrequalificationJob
    {
        public string AssignedStaff { get; set; }
    }
    public class PreqJobsModel : PageModel
    {
        private readonly DcProcurement.ProcurementDBContext _context;
        private readonly UserManager<User> _userManager;

        public PreqJobsModel(DcProcurement.ProcurementDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public IList<InputModel> InputModels { get; set; }

        public List<PrequalificationWorkflow> PrequalificationWorkflow { get; set; }
        // public IList<PrequalificationWorkflow> Steps { get; set; }
        public int Counter { get; set; }




        //get current user
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task OnGetAsync()
        {
            //get current user
            // var user = await GetCurrentUserAsync();



            //get all the steps
            try
            {
                PrequalificationWorkflow = _context.PrequalificationWorkflows.ToList();
                Counter = PrequalificationWorkflow.Max(x => x.Step);
            }
            catch (ArgumentNullException)
            {
                Counter = 0;
                throw;
            }


            //get prequalification jobs pending for user or all users
            InputModels = await _context.PrequalificationJobs.Where(x => x.Done == false)//.Where(x=> x.StaffId == user.Id || x.StaffId == null)
                .Include(p => p.CompanyInfo)
                .Include(p => p.Staff).Select(
                p => new InputModel
                {
                    CompanyInfo = p.CompanyInfo,
                    CompanyInfoId = p.CompanyInfoId,
                    CreationDate = p.CreationDate,
                    Done = p.Done,
                    DoneDate = p.DoneDate,
                    Id = p.Id,
                    Staff = p.Staff,
                    StaffId = p.StaffId,
                    WorkFlowStep = p.WorkFlowStep,
                }).ToListAsync();


            //check if the next step if it is assigned to one or multiple

            //if to be assigned to multiple staff, add checkbox to assign to staff

            //no checkbox for single staff


        }
        public void OnPost()
        {

            //TODO: refactor post code to receive values
            List<PrequalificationJob> newJobs = new List<PrequalificationJob>();

            for (int i = 0; i < InputModels.Count; i++)
            {
                //get current job
                var job = InputModels[i];


                //set current job as done 
                job.Done = true;

                //check if current job is at last workflow step:
                if (job.WorkFlowStep >= Counter)
                {
                    //dont create a new job for it.
                    return;
                }


                //check if next workflow step is assigned to staff
                //get next workflow step
                var nxtWorkflow = PrequalificationWorkflow.FirstOrDefault(w => w.Step == job.WorkFlowStep + 1);

                //check if next workflow step is assigned to a single staff
                if (nxtWorkflow.ToPersonOrAssign)
                {
                    //create new job with staff id and add to list of new jobs
                    //also increment the workflow step
                    newJobs.Add(new PrequalificationJob
                    {
                        CompanyInfoId = job.CompanyInfoId,
                        CreationDate = DateTime.Now,
                        StaffId = nxtWorkflow.StaffId,
                        WorkFlowStep = job.WorkFlowStep + 1
                        
                    });
                }
                //if not assigned
                else
                {
                    //create new job without specific staff
                    //also increment the workflow step
                    newJobs.Add(new PrequalificationJob
                    {
                        CompanyInfoId = job.CompanyInfoId,
                        CreationDate = DateTime.Now,
                        WorkFlowStep = job.WorkFlowStep + 1,
                    });
                }

                
               
            }

        }

    }
}
