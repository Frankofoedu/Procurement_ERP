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
        public DcProcurement.WorkflowCategory workflowCategory { get; set; }
        public List<WorkflowCategory> workflowCategories { get; set; }

        public void OnGet(int? id )
        {
            if (id != null)
            {
                workflowCategory = _context.WorkflowCategories.Find(id.Value);
            }
            else workflowCategory = new WorkflowCategory();

            workflowCategories = _context.WorkflowCategories.ToList();
        }

        public async void OnPost(int? id)
        {
            if (!ModelState.IsValid)
            {
                Error = "Invalid Data!";
            }
            else if (id.HasValue && workflowCategory.Id == id.Value)
            {
                var wfc = _context.WorkflowCategories.Find(id.Value);

                if (wfc!=null)
                {
                    wfc.Description = workflowCategory.Description;
                    wfc.Name = workflowCategory.Name;
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
                    _context.WorkflowCategories.Add(workflowCategory);
                    _context.SaveChanges();
                    Message = "Category added successfully.";
                }
                catch (Exception)
                {
                    Error = "An error occured during save. Please check the data and try again.";
                }
            }

            workflowCategories = _context.WorkflowCategories.ToList();
        }
    }
}