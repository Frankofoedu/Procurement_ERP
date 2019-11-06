using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.Workflow
{
    public class WorkflowStaffModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;

        public WorkflowStaffModel(ProcurementDBContext context, BSSLSYS_ITF_DEMOContext bsslContext)
        {
            _context = context;
            _bsslContext = bsslContext;
        }

        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public List<WorkflowCategoryActionStaff> newWorkflowStaff { get; set; }

        public List<DcProcurement.WorkflowCategoryActionStaff> WorkflowStaffs { get; set; }
        public WorkflowType curCategory { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            var categories = _context.WorkflowTypes;

            if (categories.Any())
            {
                curCategory = categories.First();

                WorkflowStaffs = _context.WorkflowCategoryActionStaffs.Include(m=>m.WorkflowAction).Include(n => n.Staff)
                    .Where(x=> x.WorkflowCategoryId == categories.First().Id)
                    .OrderBy(m=>m.WorkflowActionId).ToList();
            }

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            ViewData["Actions"] = new SelectList(_context.WorkflowActions, "Id", "Name");

        }
    }
}