using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.Workflow
{
    public class WorkflowSetupModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public WorkflowSetupModel(ProcurementDBContext context)
        {
            _context = context;
        }

        public class Input
        {
            public int Step { get; set; }
            public int WorkflowActionId { get; set; }
            public string Description { get; set; }
            public bool Assign { get; set; }
            public string StaffId { get; set; }
            public string StaffCode { get; set; }
            public string StaffName { get; set; }
            public string AlternativeStaffId { get; set; }
            public string AlternativeStaffCode { get; set; }
            public string AlternativeStaffName { get; set; }
        }

        [BindProperty]
        public List<Input> InputModel { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }

        public List<DcProcurement.WorkflowAction> WorkflowActions { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            WorkflowActions = _context.WorkflowActions.ToList();
            foreach (var item in WorkflowActions) item.Workflows = null;

            ViewData["Categories"] = new SelectList(_context.WorkflowCategories, "Id", "Name");

        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                WorkflowActions = _context.WorkflowActions.ToList();
                foreach (var item in WorkflowActions) item.Workflows = null;

                ViewData["Categories"] = new SelectList(_context.WorkflowCategories, "Id", "Name");
                return;
            }

            var newPWF = new List<DcProcurement.Workflow>();

            for (var i = 0; i < InputModel.Count; i++)
            {
                var item = InputModel[i];

                if (!string.IsNullOrWhiteSpace(item.Description))
                {
                    if (item.Assign && string.IsNullOrWhiteSpace(item.StaffName))
                    {
                        Error = "An Error Occured. Make sure that if 'For Specific Staff' field is checked, a valid staff code is Entered.";
                        WorkflowActions = _context.WorkflowActions.ToList();
                        foreach (var x in WorkflowActions) x.Workflows = null;

                        ViewData["Categories"] = new SelectList(_context.WorkflowCategories, "Id", "Name");
                        return;
                    }

                    var pwf = new DcProcurement.Workflow()
                    {
                        Description = item.Description,
                        ToPersonOrAssign = item.Assign,
                        WorkflowCategoryId = CategoryId,
                        WorkflowActionId = item.WorkflowActionId,

                        Step = i + 1,
                    };

                    if (item.Assign)
                    {
                        pwf.StaffId = item.StaffId;
                        pwf.AlternativeStaffId = item.AlternativeStaffId;
                    }

                    newPWF.Add(pwf);
                }
            }

            if (newPWF.Count < 1)
            {
                Error = "An Error Occured. Make sure make sure one or more workflow steps are added.";
                WorkflowActions = _context.WorkflowActions.ToList();
                foreach (var item in WorkflowActions) item.Workflows = null;

                ViewData["Categories"] = new SelectList(_context.WorkflowCategories, "Id", "Name");
                return;
            }

            var curWF = _context.Workflows.Where(m => m.WorkflowCategoryId == CategoryId).OrderBy(n => n.Step).ToList();
            _context.Workflows.RemoveRange(curWF);
            _context.Workflows.AddRange(newPWF);

            _context.SaveChanges();

            InputModel = null;
            WorkflowActions = _context.WorkflowActions.ToList();
            foreach (var item in WorkflowActions) item.Workflows = null;

            ViewData["Categories"] = new SelectList(_context.WorkflowCategories, "Id", "Name");

            Message = "Saved Successfully";
        }
    }
}