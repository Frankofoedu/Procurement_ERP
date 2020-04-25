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
    public class WorkflowCategoryModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public WorkflowCategoryModel(ProcurementDBContext context)
        {
            _context = context;
        }

        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public DcProcurement.WorkflowType workflowType { get; set; }
        public List<WorkflowType> workflowTypes { get; set; }

        public void OnGet(int? id )
        {
            if (id != null)
            {
                workflowType = _context.WorkflowTypes.Find(id.Value);
            }
           // else workflowType = new WorkflowType();

            workflowTypes = _context.WorkflowTypes.OrderBy(x => x.Code).ToList();
        }

        public async void OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                Error = "Invalid Data!";
            }
            else if (id.HasValue && workflowType.Id == id.Value)
            {
                var wfc = _context.WorkflowTypes.Find(id.Value);

                if (wfc!=null)
                {
                    wfc.Description = workflowType.Description;
                    wfc.Name = workflowType.Name;
                    _context.SaveChanges();

                    Message = "Update was successful.";
                }
                else
                {
                    Error = "Category Not Found!";
                }
            }
            else
            {
                try
                {
                    _context.WorkflowTypes.Add(workflowType);
                    _context.SaveChanges();
                    Message = "Category added successfully.";
                }
                catch (Exception)
                {
                    Error = "An error occured during save. Please check the data and try again.";
                }
            }

            workflowTypes = _context.WorkflowTypes.ToList();
        }
    }
}