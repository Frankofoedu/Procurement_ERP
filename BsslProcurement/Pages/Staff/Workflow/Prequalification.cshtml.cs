using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.Workflow
{
    public class PrequalificationModel : PageModel
    {

        private readonly ProcurementDBContext _context;

        public PrequalificationModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<PrequalificationWorkflow> newPrequalificationWorkflows { get; set; }

        public List<PrequalificationWorkflow> currentPrequalificationWorkflows { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            currentPrequalificationWorkflows = _context.PrequalificationWorkflows.Include(m=>m.StaffToAssign).ToList();
        }
    }
}