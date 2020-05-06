using System;
using System.Collections.Generic;
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
        public int curWorkflowId { get; set; }
        [BindProperty]
        public WorkflowStaff newWorkflowStaff { get; set; }

        public List<DcProcurement.WorkflowStaff> WorkflowStaffs { get; set; }
        public DcProcurement.Workflow curWorkflow { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string msg, int? eid)
        {
            if (id == null)
            {
                return LocalRedirect("WorkflowSetup");
            }
            Message = msg;

            if (eid!=null)
            {
                newWorkflowStaff = await _context.WorkflowStaffs.Include(n => n.Staff).FirstOrDefaultAsync(m => m.Id == eid.Value);
            }
            await InitializeAsync(id.Value);

            return Page();
        }

        private async Task InitializeAsync(int id)
        {
            curWorkflowId = id;
            curWorkflow = await _context.Workflows.Include(m=>m.WorkflowAction).Include(m=>m.WorkflowType).FirstOrDefaultAsync(n=>n.Id == id);

            if (curWorkflow != null)
            {
                WorkflowStaffs = await _context.WorkflowStaffs.Include(n => n.Staff)
                    .Where(x => x.WorkflowId == curWorkflowId).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var categories = _context.WorkflowTypes;

            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
            }
            else
            {
                if (newWorkflowStaff.Id <= 0)
                {
                    var WS = new WorkflowStaff()
                    {
                        WorkflowId = curWorkflowId,
                        Threshold = newWorkflowStaff.Threshold,
                        MustApprove= newWorkflowStaff.MustApprove,
                        CanModify = newWorkflowStaff.CanModify,
                    };

                    var staff = (await _context.Staffs.FirstOrDefaultAsync(m => m.StaffCode == newWorkflowStaff.StaffId.Trim()));
                    if (staff != null)
                    {
                        WS.StaffId = staff.Id;


                        _context.WorkflowStaffs.Add(WS);
                        _context.SaveChanges();

                        return RedirectToPage("./WorkflowStaff", new { id = curWorkflowId, msg = "Saved Successfully" });
                    }
                    else
                    {
                        Error = "Staff not found.";
                    }
                }
                else
                {
                    var ws = await _context.WorkflowStaffs.Include(n => n.Staff).FirstOrDefaultAsync(m => m.Id == newWorkflowStaff.Id);

                    ws.Threshold = newWorkflowStaff.Threshold;
                    ws.MustApprove = newWorkflowStaff.MustApprove;
                    ws.CanModify = newWorkflowStaff.CanModify;

                    await _context.SaveChangesAsync();
                    return RedirectToPage("./WorkflowStaff", new { id = curWorkflowId, msg = "Updated Successfully" });
                }

            }

            await InitializeAsync(curWorkflowId);

            return Page();
        }



    }
}