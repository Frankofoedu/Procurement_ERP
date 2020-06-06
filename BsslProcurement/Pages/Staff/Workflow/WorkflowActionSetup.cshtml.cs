using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public List<bool> workflowActionDeletable { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id != null)
            {
                workflowAction = await _context.WorkflowActions.FirstOrDefaultAsync(m => m.Id == id.Value);
            }
            else workflowAction = new WorkflowAction();

            workflowActions = await _context.WorkflowActions.Include(m => m.Workflows).Where(m => m.Name != Constants.InitiatorActionName).ToListAsync();
        }

        public async Task OnPost(int? id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Error = "Invalid Data!";
                }
                else if (id.HasValue && workflowAction.Id == id.Value)
                {
                    var wfa = await _context.WorkflowActions.FirstOrDefaultAsync(m => m.Id == id.Value);

                    if (wfa != null)
                    {
                        wfa.Description = workflowAction.Description;
                        wfa.Name = workflowAction.Name;
                        await _context.SaveChangesAsync();

                        Message = "Update was successful.";
                    }
                    else
                    {
                        Error = "Action Not Found!";
                    }
                }
                else
                {
                    var check = await _context.WorkflowActions.AnyAsync(m => m.Name == workflowAction.Name);
                    if (check)
                    {
                        Error = "The Workflow Action Name already exists.";
                    }
                    else
                    {
                        _context.WorkflowActions.Add(workflowAction);
                        await _context.SaveChangesAsync();
                        Message = "Workflow Action added successfully.";
                    }
                }
            }
            catch (Exception)
            {
                Error = "An error occured during save. Please check the data and try again.";
            }
            finally
            {
                workflowActions = await _context.WorkflowActions.Include(m => m.Workflows).Where(m => m.Name != Constants.InitiatorActionName).ToListAsync();
            }

        }
    }
}