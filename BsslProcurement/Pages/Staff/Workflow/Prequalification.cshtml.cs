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

        public class Input
        {
            public int Step { get; set; }
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

        public List<PrequalificationWorkflow> currentPrequalificationWorkflows { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {
            currentPrequalificationWorkflows = _context.PrequalificationWorkflows.Include(m=>m.StaffToAssign).ToList();
        }


        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                currentPrequalificationWorkflows = _context.PrequalificationWorkflows.Include(m => m.StaffToAssign).ToList();
                return;
            }

            var newPWF = new List<PrequalificationWorkflow>();

            for (var i= 0; i< InputModel.Count; i++)
            {
                var item = InputModel[i];

                if (!string.IsNullOrWhiteSpace(item.Description))
                {
                    if (item.Assign && string.IsNullOrWhiteSpace(item.StaffName))
                    {
                        Error = "An Error Occured. Make sure that if 'For Specific Staff' field is checked, a valid staff code is Entered.";
                        currentPrequalificationWorkflows = _context.PrequalificationWorkflows.Include(m => m.StaffToAssign).ToList();
                        return;
                    }

                    var pwf = new PrequalificationWorkflow()
                    {
                        Description = item.Description,
                        ToPersonOrAssign = item.Assign,
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

            if (newPWF.Count<1)
            {
                Error = "An Error Occured. Make sure make sure one or more workflow steps are added.";
                currentPrequalificationWorkflows = _context.PrequalificationWorkflows.Include(m => m.StaffToAssign).ToList();
                return;
            }

            var curWF = _context.PrequalificationWorkflows.ToList();
            _context.PrequalificationWorkflows.RemoveRange(curWF);
            _context.PrequalificationWorkflows.AddRange(newPWF);

            _context.SaveChanges();

            InputModel = null;
            currentPrequalificationWorkflows = _context.PrequalificationWorkflows.Include(m => m.StaffToAssign).ToList();

            Message = "Saved Successfully";
        }
        //TODO: Add code to move all companies to least workflow
    }
}