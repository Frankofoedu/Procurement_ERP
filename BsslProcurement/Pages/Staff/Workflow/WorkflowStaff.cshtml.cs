using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using DcProcurement.ViewModels;
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
        public int curCategoryId { get; set; }
        [BindProperty]
        public WorkflowCategoryActionStaff newWorkflowStaff { get; set; }

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
                curCategoryId = curCategory.Id;

                WorkflowStaffs = _context.WorkflowCategoryActionStaffs.Include(m=>m.WorkflowAction).Include(n => n.Staff)
                    .Where(x=> x.WorkflowTypeId == categories.First().Id)
                    .OrderBy(m=>m.WorkflowActionId).ToList();
            }

            ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            ViewData["Actions"] = new SelectList(_context.WorkflowActions, "Id", "Name");

        }

        private void Initialize()
        {

        }

        public void OnPost()
        {
            var categories = _context.WorkflowTypes;

            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
            }
            else
            {
                var WS = new WorkflowCategoryActionStaff()
                {
                    WorkflowTypeId = curCategoryId,
                    WorkflowActionId = newWorkflowStaff.WorkflowActionId
                };
                var staffid = _context.Staffs.First(m => m.StaffCode == newWorkflowStaff.StaffId).Id;
                if (!string.IsNullOrWhiteSpace(staffid))
                {
                    WS.StaffId = staffid;
                }
                _context.WorkflowCategoryActionStaffs.Add(WS);

                _context.SaveChanges();

                Message = "Saved Successfully";
            }

            if (categories.Any())
            {
                curCategory = categories.First(m => m.Id == curCategoryId);

                WorkflowStaffs = _context.WorkflowCategoryActionStaffs.Include(m => m.WorkflowAction).Include(n => n.Staff)
                    .Where(x => x.WorkflowTypeId == curCategoryId)
                    .OrderBy(m => m.WorkflowActionId).ToList();

                newWorkflowStaff = new WorkflowCategoryActionStaff();
            }
            ViewData["Categories"] = new SelectList(categories, "Id", "Name", curCategoryId);
            ViewData["Actions"] = new SelectList(_context.WorkflowActions, "Id", "Name");
        }





        public PartialViewResult OnGetStaffPartial()
        {
            //get all staff and thier ranks
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { Staff = x, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToList();

            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }

    }
}