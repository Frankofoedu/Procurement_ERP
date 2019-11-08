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
        public async Task<IActionResult> GetWorkflowAsync([FromRoute] int id) //id is the workflow category id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Workflows = await _context.Workflows.Include(m => m.WorkflowAction).Include(m => m.WorkflowType).Where(m=>m.WorkflowTypeId == id).ToListAsync();

            if (Workflows == null)
            {
                return NotFound();
            }

            foreach (var item in Workflows)
            {
                item.WorkflowAction.Workflows = null;
                item.WorkflowType.Workflows = null;
                
            }

            return Ok(Workflows);
        }

        // GET: api/Workflow/WorkflowStaff/5
        [HttpGet("WorkflowStaff/{id}")]
        public async Task<IActionResult> GetWorkflowStaffAsync([FromRoute] int id) //id is the workflow category id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkflowStaffs = await _context.WorkflowStaffs.Include(n => n.Staff)
                    .Where(x => x.WorkflowId == id).ToListAsync();

            return Ok(WorkflowStaffs);
        }
    }
}