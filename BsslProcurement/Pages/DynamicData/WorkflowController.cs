using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.DynamicData
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly ProcurementDBContext _context;

        public WorkflowController(ProcurementDBContext context)
        {
            _context = context;
        }

        // GET: api/Workflow/5
        [HttpGet("{id}")]
        public IActionResult GetWorkflow([FromRoute] int id) //id is the workflow category id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Workflows = _context.Workflows.Include(n=>n.WorkflowAction).Include(m => m.StaffToAssign).Where(m=>m.WorkflowCategoryId == id).ToList();

            if (Workflows == null)
            {
                return NotFound();
            }

            foreach (var item in Workflows)
            {
                item.WorkflowAction.Workflows = null;
                item.WorkflowCategory = null;
                if (item.AlternativeStaffToAssign !=null)
                {
                    item.AlternativeStaffToAssign.StaffWorkflows = null;
                    item.AlternativeStaffToAssign.AdditionalStaffWorkflows = null;
                }
                if (item.StaffToAssign != null)
                {
                    item.StaffToAssign.StaffWorkflows = null;
                    item.StaffToAssign.AdditionalStaffWorkflows = null;
                }
            }

            return Ok(Workflows);
        }

    }
}