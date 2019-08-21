using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.Workflow
{
    [Authorize]
    public class WorkflowActionSetupModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public WorkflowActionSetupModel(ProcurementDBContext context)
        {
            _context = context;
        }

        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public WorkflowAction workflowAction { get; set; }
        public List<WorkflowAction> workflowActions { get; set; }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                workflowAction = _context.WorkflowActions.Find(id.Value);
            }
            else workflowAction = new WorkflowAction();

            workflowActions = _context.WorkflowActions.ToList();
        }

        public async void OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                Error = "Invalid Data!";
            }
            else if (id.HasValue && workflowAction.Id == id.Value)
            {
                var wfa = _context.WorkflowActions.Find(id.Value);

                if (wfa != null)
                {
                    wfa.Description = workflowAction.Description;
                    wfa.Name = workflowAction.Name;
                    _context.SaveChanges();

                    Message = "Update was successful.";
                }
                else
                {
                    Error = "Action Not Found!";
                }
            }
            else
            {
                try
                {
                    _context.WorkflowActions.Add(workflowAction);
                    _context.SaveChanges();
                    Message = "Workflow Action added successfully.";
                }
                catch (Exception)
                {
                    Error = "An error occured during save. Please check the data and try again.";
                }
            }

            workflowActions = _context.WorkflowActions.ToList();
        }
    }
}