using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DcProcurement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BsslProcurement.Pages.Staff.JobViews
{
  
    public class InputModel : PrequalificationJob
    {
        public string AssignedStaff { get; set; }
    }

    public class CheckInput
    {
        public int JobId { get; set; }
        public bool Checked { get; set; } = false;
    }

    public class JobGroup
    {
        public string todo { get; set; }
        public DcProcurement.Workflow WorkflowStep { get; set; }
        public List<PrequalificationJob> Jobs { get; set; }
    }
    [Authorize]
    public class PreqJobsModel : PageModel
    {
        private readonly DcProcurement.ProcurementDBContext _context;
        private readonly UserManager<User> _userManager;

        public PreqJobsModel(DcProcurement.ProcurementDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //[BindProperty]
        //public IList<InputModel> InputModels { get; set; }

        [BindProperty]
        public List<CheckInput> CheckInputs { get; set; }
        [BindProperty]
        public string staffId { get; set; }


        public List<JobGroup> JobGroups { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public List<DcProcurement.Workflow> PrequalificationWorkflow { get; set; }


        //get current user
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task OnGetAsync()
        {
            await newLoadJobs();

        }

        private async Task newLoadJobs()
        {
            //get current user
            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                //get all the workflow steps
                var allWorkflow = _context.Workflows.Where(m=>m.WorkflowType.Name == "procurement").OrderBy(n=>n.Step).ToList();
                //get all prequalification jobs pending for user or all users
                var allJobs = await _context.PrequalificationJobs.Where(x => x.JobStatus == Enums.JobState.NotDone).Where(x => x.StaffId == user.Id || x.StaffId == null)
                    .Include(p => p.CompanyInfo)
                    .Include(p => p.Staff).ToListAsync();

                JobGroups = new List<JobGroup>();

                //group allJobs into jobGroups by steps
                for (int i = 0; i < allWorkflow.Count; i++)
                {
                    var curWorkFlowStep = allWorkflow[i];

                    DcProcurement.Workflow nextWorkFlowStep;
                    var todo = "";

                    if ((i + 1) < allWorkflow.Count)
                    {
                        nextWorkFlowStep = allWorkflow[i + 1];

                    }
                    else
                    { todo = "approve"; }


                    //var stepJobs = allJobs.Where(m => m.WorkFlowStep == curWorkFlowStep.Step).ToList();

                    //if (stepJobs.Count > 0)
                    //{
                    //    var jg = new JobGroup()
                    //    {
                    //        Jobs = stepJobs,
                    //        WorkflowStep = curWorkFlowStep,
                    //        todo = todo,
                    //    };
                    //    JobGroups.Add(jg);
                    //}
                }

                //var step0jobs = allJobs.Where(m => m.WorkFlowStep == 0).ToList();

                //if (step0jobs.Count > 0)
                //{
                //    var jg = new JobGroup()
                //    {
                //        Jobs = step0jobs,
                //        WorkflowStep = new DcProcurement.Workflow()
                //        {
                //            Step = 0,
                //        },
                //        todo = "approve",
                //    };
                //    JobGroups.Add(jg);
                //}
            }
          
           

        }

        //private async Task LoadJobs()
        //{
        //    //get current user
        //    var user = await GetCurrentUserAsync();



        //    //get all the steps
        //    try
        //    {
        //        PrequalificationWorkflow = _context.PrequalificationWorkflows.ToList();
        //        Counter = PrequalificationWorkflow.Max(x => x.Step);
        //    }
        //    catch (ArgumentNullException)
        //    {
        //        Counter = 0;
        //        throw;
        //    }


        //    //get prequalification jobs pending for user or all users
        //    InputModels = await _context.PrequalificationJobs.Where(x => x.Done == false).Where(x=> x.StaffId == user.Id || x.StaffId == null)
        //        .Include(p => p.CompanyInfo)
        //        .Include(p => p.Staff).Select(
        //        p => new InputModel
        //        {
        //            CompanyInfo = p.CompanyInfo,
        //            CompanyInfoId = p.CompanyInfoId,
        //            CreationDate = p.CreationDate,
        //            Done = p.Done,
        //            DoneDate = p.DoneDate,
        //            Id = p.Id,
        //            Staff = p.Staff,
        //            StaffId = p.StaffId,
        //            WorkFlowStep = p.WorkFlowStep,
        //        }).ToListAsync();
        //}

        public async Task OnPost(int? postedStep)
        {
            if (!ModelState.IsValid)
            {
                await newLoadJobs();
            }
            else
            {
                if (postedStep != null)
                {
                    try
                    {
                        await ProcessJobsAsync(postedStep.Value);

                        _context.SaveChanges();
                        Message = "Tasks Processed";

                        await newLoadJobs();

                    }
                    catch (Exception)
                    {
                        Error = "An error occured";
                    }
                }
            }

        }

        private async Task ProcessJobsAsync(int step)
        {
            //get current user
            var user = await GetCurrentUserAsync();

            //get all prequalification jobs pending for user or all users
            var allJobs = await _context.PrequalificationJobs.Where(x => x.JobStatus == Enums.JobState.NotDone).Where(x => x.StaffId == user.Id || x.StaffId == null)
                .Include(p => p.CompanyInfo).ToListAsync();

            //get all the workflow steps
            var allWorkflow = _context.Workflows.Where(m => m.WorkflowType.Name == "procurement").OrderBy(n => n.Step).ToList();

            foreach (var item in CheckInputs)
            {
                if (!item.Checked)
                { continue; }

                var job = allJobs.FirstOrDefault(m=> m.Id == item.JobId);

                if (job != null)
                {
                    //var nextStep = allWorkflow.FirstOrDefault(m => m.Step == job.WorkFlowStep + 1);

                    //if (nextStep != null)
                    //{
                    //    //if (!nextStep.ToPersonOrAssign)
                    //    //{
                    //    //    if (staffId != null)
                    //    //    {
                    //    //        job.Done = true;
                    //    //        job.DoneDate = DateTime.UtcNow;

                    //    //        var newJob = new PrequalificationJob()
                    //    //        {
                    //    //            CompanyInfoId = job.CompanyInfoId,
                    //    //            CreationDate = DateTime.UtcNow,
                    //    //            StaffId = staffId,
                    //    //            WorkFlowStep = job.WorkFlowStep+1,
                    //    //        };

                    //    //        _context.PrequalificationJobs.Add(newJob);
                    //    //    }
                    //    //}
                    //}
                }
            }


        }
    }
}
