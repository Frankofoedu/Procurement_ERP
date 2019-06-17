using System;
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


        [BindProperty]
        public PrequalificationJob Job { get; set; }

        [BindProperty]
        public CompanyInfo CompanyInfo { get; set; }


        public PrequalificationWorkflow Step { get; set; }


        public string Message { get; set; }
        public string Error { get; set; }
        public string baseURL { get; set; }
        public int CategoryCount { get; set; }

        [BindProperty]
        public List<int> subCatsId { get; set; }

        public List<SubmittedCriteria> SubmittedCriterias { get; set; }


        public IActionResult OnGet(int? id)
        {
            if (id == null)
            { return LocalRedirect("~/Staff/JobViews/PreqJobs"); }

            Job = _context.PrequalificationJobs.FirstOrDefault(n=>n.Id == id);
            if (Job == null)
            { return LocalRedirect("~/Staff/JobViews/PreqJobs"); }

            if (Job.Done)
            { return LocalRedirect("~/Staff/JobViews/PreqJobs"); }

            CompanyInfo = _context.CompanyInfo.Include(n=>n.CompanyInfoSelectedSubcategory).Include(b=>b.EquipmentDetails).Include(l=>l.ExperienceRecords)
                .Include(k=>k.PersonnelDetails).FirstOrDefault(m => m.Id == Job.CompanyInfoId);

            SubmittedCriterias = _context.SubmittedCriteria.Include(n => n.Criteria).Where(m => m.CompanyInfoId == CompanyInfo.Id).ToList();

            Step = _context.PrequalificationWorkflows.FirstOrDefault(m => m.Step == Job.WorkFlowStep);
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

            return Page();
        }

        /// <summary>
        /// loads the categories
        /// </summary>

    }
}