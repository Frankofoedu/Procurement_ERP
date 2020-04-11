using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using DcProcurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly ProcurementDBContext _context;
        private readonly IWorkFlowService _workflowService;
        public WorkflowController(ProcurementDBContext context, IWorkFlowService workflowService)
        {
            _context = context;
            _workflowService = workflowService;
        }

        // GET: api/Workflow/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkflowAsync([FromRoute] int id) //id is the workflow category id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Workflows = await _context.Workflows.Include(m => m.WorkflowAction).Include(m => m.WorkflowType).Where(m => m.WorkflowTypeId == id).ToListAsync();

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

        // Put: api/Workflow/WorkflowStaff/Suspend/5
        [HttpPut("WorkflowStaff/Suspend/{WorkflowStaffId}")]
        public async Task<IActionResult> PutSuspendWorkflowStaffAsync([FromRoute] int WorkflowStaffId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkflowStaff = await _context.WorkflowStaffs.FindAsync(WorkflowStaffId);
            WorkflowStaff.State = Enums.WorkflowStaffState.Suspended;

            await _context.SaveChangesAsync();

            return Ok("Success");
        }

        // Put: api/Workflow/WorkflowStaff/Suspend/5
        [HttpPut("WorkflowStaff/Unsuspend/{WorkflowStaffId}")]
        public async Task<IActionResult> PutUnSuspendWorkflowStaffAsync([FromRoute] int WorkflowStaffId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkflowStaff = await _context.WorkflowStaffs.FindAsync(WorkflowStaffId);
            WorkflowStaff.State = Enums.WorkflowStaffState.Normal;

            await _context.SaveChangesAsync();

            return Ok("Success");
        }

        // Delete: api/Workflow/WorkflowStaff/5
        [HttpDelete("WorkflowStaff/{WorkflowStaffId}")]
        public async Task<IActionResult> DeleteWorkflowStaffAsync([FromRoute] int WorkflowStaffId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkflowStaff = await _context.WorkflowStaffs.FindAsync(WorkflowStaffId);
            _context.WorkflowStaffs.Remove(WorkflowStaff);
            await _context.SaveChangesAsync();

            return Ok("Success");
        }

        // GET: api/Workflow/WorkflowStaff/5
        [HttpGet("WorkflowForAction/{action}/{workflowId}/{workflowTypeId}")]
        public async Task<IActionResult> GetWorkflowForActionAsync([FromRoute] string action, [FromRoute] int workflowId, [FromRoute] int workflowTypeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Workflows = new List<ViewModels.WorkFlowTypesViewModel>();

            if (action.ToLower() == "next")
            {
                Workflows = await _workflowService.GetNextWorkActionflowStepsAsync(workflowId, workflowTypeId);
            }
            else if (action.ToLower() == "previous")
            {
                Workflows = await _workflowService.GetPreviousWorkActionflowStepsAsync(workflowId, workflowTypeId);
            }

            return Ok(Workflows);
        }

    }
}