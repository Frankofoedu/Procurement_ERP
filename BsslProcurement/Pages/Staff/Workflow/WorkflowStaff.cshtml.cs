using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        public int curWorkflowId { get; set; }
        [BindProperty]
        public WorkflowStaff newWorkflowStaff { get; set; }

        public List<DcProcurement.WorkflowStaff> WorkflowStaffs { get; set; }
        public DcProcurement.Workflow curWorkflow { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return LocalRedirect("WorkflowSetup");
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

        public async Task OnPostAsync()
        {
            var categories = _context.WorkflowTypes;

            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
            }
            else
            {
                var WS = new WorkflowStaff()
                {
                    WorkflowId = curWorkflowId,
                };
                var staffid = (await _context.Staffs.FirstOrDefaultAsync(m => m.StaffCode == newWorkflowStaff.StaffId)).Id;
                if (!string.IsNullOrWhiteSpace(staffid))
                {
                    WS.StaffId = staffid;

                    _context.WorkflowStaffs.Add(WS);
                    _context.SaveChanges();

                    Message = "Saved Successfully";
                }
                else
                {
                    Error = "Staff not found.";
                }

            }

            await InitializeAsync(curWorkflowId);
        }





        public PartialViewResult OnGetStaffPartial()
        {
            //get all staff and thier ranks
            
            ///TODO
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffName = x.Othernames, StaffCode = x.Staffid, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToList();

            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }

    }
}