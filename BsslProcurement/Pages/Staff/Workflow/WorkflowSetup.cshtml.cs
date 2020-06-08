using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.Workflow
{
    [Authorize]
    public class WorkflowSetupModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;

        public WorkflowSetupModel(ProcurementDBContext context, BSSLSYS_ITF_DEMOContext bsslContext)
        {
            _context = context;
            _bsslContext = bsslContext;
        }

        public class Input
        {
            public int Step { get; set; }
            [Required(ErrorMessage ="Select Action")]
            public int WorkflowActionId { get; set; }
        }

        [BindProperty]
        public List<Input> InputModel { get; set; }
        [BindProperty]
        public int CategoryId { get; set; }
        [BindProperty]
        public string CategoryName { get; set; }

        public List<DcProcurement.WorkflowAction> WorkflowActions { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public async Task OnGetAsync()
        {
            WorkflowActions = await _context.WorkflowActions.Where(m=>m.Name != Constants.InitiatorActionName).ToListAsync();
            foreach (var item in WorkflowActions) item.Workflows = null;

            ViewData["Categories"] = new SelectList(_context.WorkflowTypes, "Id", "Name");
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                WorkflowActions = await _context.WorkflowActions.Where(m => m.Name != Constants.InitiatorActionName).ToListAsync();
                foreach (var item in WorkflowActions) item.Workflows = null;

                ViewData["Categories"] = new SelectList(_context.WorkflowTypes, "Id", "Name");
                return;
            }

            var newPWF = new List<DcProcurement.Workflow>();
            var count = 0;

            if (CategoryName == Constants.RequisitionWorkflow)
            {
                var initiatorAction = await _context.WorkflowActions.FirstOrDefaultAsync(m=>m.Name == Constants.InitiatorActionName);
                count++;

                var initiatorWf = new DcProcurement.Workflow()
                {
                    WorkflowTypeId = CategoryId,
                    WorkflowActionId = initiatorAction.Id,

                    Step = count,
                };
                newPWF.Add(initiatorWf);
            }

            for (var i = 0; i < InputModel.Count; i++)
            {
                var item = InputModel[i];

                var pwf = new DcProcurement.Workflow()
                {
                    WorkflowTypeId = CategoryId,
                    WorkflowActionId = item.WorkflowActionId,

                    Step = i + count + 1,
                };

                newPWF.Add(pwf);
            }

            if (newPWF.Count < 1)
            {
                Error = "An Error Occured. Make sure make sure one or more workflow steps are added.";
                WorkflowActions = await _context.WorkflowActions.Where(m => m.Name != Constants.InitiatorActionName).ToListAsync();
                foreach (var item in WorkflowActions) item.Workflows = null;

                ViewData["Categories"] = new SelectList(_context.WorkflowTypes, "Id", "Name");
                return;
            }

            var curWF = await _context.Workflows.Where(m => m.WorkflowTypeId == CategoryId).OrderBy(n => n.Step).ToListAsync();
            _context.Workflows.RemoveRange(curWF);
            _context.Workflows.AddRange(newPWF);

            _context.SaveChanges();

            InputModel = null;
            WorkflowActions = await _context.WorkflowActions.Where(m => m.Name != Constants.InitiatorActionName).ToListAsync();
            foreach (var item in WorkflowActions) item.Workflows = null;

            ViewData["Categories"] = new SelectList(_context.WorkflowTypes, "Id", "Name", CategoryId);

            Message = "Saved Successfully";
        }

        public PartialViewResult OnGetStaffPartial()
        {

            //get all staff and thier ranks
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffCode = x.Staffid, StaffName = x.Othernames, Rank = _bsslContext.Codestab.Where(m => m.Option1 == "f4" && m.Code == x.Positionid).FirstOrDefault().Desc1 }).ToListAsync();


            //           var  = _bsslContext.Stafftab.ToListAsync();
            return new PartialViewResult
            {
                ViewName = "_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }

    }
}