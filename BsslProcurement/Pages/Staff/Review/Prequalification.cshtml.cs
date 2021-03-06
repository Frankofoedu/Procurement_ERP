﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BsslProcurement.Pages.Staff.Review
{
    [Authorize]
    public class PrequalificationModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<PrequalificationModel> _logger;
        private readonly UserManager<User> _userManager;
        private readonly HttpContext currentContext;

        public readonly IHostingEnvironment _hostingEnvironment;
        public PrequalificationModel(ProcurementDBContext context, IHostingEnvironment hostingEnvironment,
                                    UserManager<User> userManager, SignInManager<User> signInManager,
                                    ILogger<PrequalificationModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            currentContext = httpContextAccessor.HttpContext;
        }

        public class filesApproved
        {
            public int Id { get; set; }
            public Enums.VerificationStates VeriState { get; set; }
        }



        public CompanyInfo CompanyInfo { get; set; }
        public List<SubmittedCriteria> SubmittedCriterias { get; set; }


        public string Message { get; set; }
        public string Error { get; set; }
        public string baseURL { get; set; }
        public int CategoryCount { get; set; }
        public string todo { get; set; }
        public string Description { get; set; }


        [BindProperty]
        public string Remark { get; set; }
        [BindProperty]
        public int JobId { get; set; }
        [BindProperty]
        public bool CompanyApproved { get; set; }
        [BindProperty]
        public List<filesApproved> submittedCriteriaApproveds { get; set; }
        [BindProperty]
        public List<filesApproved> personnelFilesApproveds { get; set; }
        [BindProperty]
        public List<DcProcurement.Staff> Staffs { get; set; }

        [BindProperty]
        public string nxtStaffId { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            { return LocalRedirect("~/Staff/JobViews/PreqJobs"); }

            var Job = _context.PrequalificationJobs.FirstOrDefault(n=>n.Id == id);
            if (Job == null)
            { return LocalRedirect("~/Staff/JobViews/PreqJobs"); }

            if (Job.JobStatus == Enums.JobState.Done)
            { return LocalRedirect("~/Staff/JobViews/PreqJobs"); }

            JobId = Job.Id;

            CompanyInfo = _context.CompanyInfo.Include(n=>n.CompanyInfoSelectedSubcategory).Include(b=>b.EquipmentDetails).Include(l=>l.ExperienceRecords)
                .Include(k=>k.PersonnelDetails).FirstOrDefault(m => m.Id == Job.CompanyInfoId);

            SubmittedCriterias = _context.SubmittedCriteria.Include(n => n.Criteria).Where(m => m.CompanyInfoId == CompanyInfo.Id).ToList();
            submittedCriteriaApproveds = new List<filesApproved>();

            foreach (var item in SubmittedCriterias)
            {
                var FA = new filesApproved();

                FA.Id = item.Id;
                FA.VeriState = item.VerificationState;

                submittedCriteriaApproveds.Add(FA);
            }

            personnelFilesApproveds = new List<filesApproved>();
            foreach (var item in CompanyInfo.PersonnelDetails)
            {
                var FA = new filesApproved();

                FA.Id = item.Id;
                FA.VeriState = item.VerificationState;

                personnelFilesApproveds.Add(FA);
            }

         //   var curStep = _context.Workflows.FirstOrDefault(m => m.WorkflowType.Name=="procurement" && m.Step == Job.WorkFlowStep);

            //if (curStep == null) // if there is no workflow in the db
            //{
            //    Description = "Validate Files and Approve Company";
            //    todo = "approve";
            //}
            //else {
            //    Description = curStep.WorkflowAction.Description;

            //    var nextStep = _context.Workflows
            //        .FirstOrDefault(m => m.WorkflowType.Name=="procurement" && m.Step == Job.WorkFlowStep + 1);

            //    //if (nextStep == null)
            //    //{ todo = "approve"; }
            //    //else if (nextStep.ToPersonOrAssign)
            //    //{ todo = "saventoperson";
            //    //    Staffs = new List<DcProcurement.Staff>();
            //    //    Staffs.Add(nextStep.StaffToAssign);
            //    //    Staffs.Add(nextStep.AlternativeStaffToAssign);
            //    //}
            //    //else {
            //    //    todo = "saventoassign";
            //    //}
                
            //}

            baseURL = GetBaseUrl();

            return Page();
        }

        public string GetBaseUrl()
        {
            var request = currentContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured! Data not formatted properly.";
                return Page();
            }
            else
            {
                var Job = _context.PrequalificationJobs.FirstOrDefault(n => n.Id == JobId);

                CompanyInfo = _context.CompanyInfo.Include(n => n.CompanyInfoSelectedSubcategory).Include(b => b.EquipmentDetails).Include(l => l.ExperienceRecords)
                    .Include(k => k.PersonnelDetails).FirstOrDefault(m => m.Id == Job.CompanyInfoId);

                SubmittedCriterias = _context.SubmittedCriteria.Include(n => n.Criteria).Where(m => m.CompanyInfoId == CompanyInfo.Id).ToList();
                


                foreach (var item in SubmittedCriterias)
                {
                    var FA = submittedCriteriaApproveds.FirstOrDefault(m => m.Id == item.Id);

                    if (FA != null)
                    {
                        item.VerificationState = FA.VeriState;
                    }
                }

                foreach (var item in CompanyInfo.PersonnelDetails)
                {
                    var FA = personnelFilesApproveds.FirstOrDefault(m => m.Id == item.Id);

                    if (FA != null)
                    {
                        item.VerificationState = FA.VeriState;
                    }
                }

                //var nextStep = _context.Workflows.FirstOrDefault(m => m.WorkflowType.Name == "procurement" && m.Step == Job.WorkFlowStep + 1);

                //if (nextStep == null)
                //{
                //    CompanyInfo.Approved = CompanyApproved;

                //    //Job.Done = true;
                //    //Job.DoneDate = DateTime.UtcNow;
                //}

                //if (nextStep.ToPersonOrAssign)
                //{
                //    Job.Done = true;
                //    Job.DoneDate = DateTime.UtcNow;

                //    var nextJob = new PrequalificationJob()
                //    {
                //        CreationDate = DateTime.UtcNow,
                //        Done = false,
                //        StaffId = nxtStaffId,
                //        WorkFlowStep = nextStep.Step,
                //        CompanyInfoId = CompanyInfo.Id
                //    };

                //    _context.PrequalificationJobs.Add(nextJob);
                //}

                //Job.Remark = Remark;

                _context.SaveChanges();

                return LocalRedirect("~/Staff/JobViews/PreqJobs");
            }



        }

        /// <summary>
        /// loads the categories
        /// </summary>

    }
}