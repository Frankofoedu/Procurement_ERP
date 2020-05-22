using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Services
{
    public class WorkFlowService : IWorkFlowService
    {

        protected readonly ProcurementDBContext _procurementDBContext;

        public WorkFlowService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }
        /// <summary>
        /// Gets all future steps for a workflow
        /// </summary>
        /// <param name="id">current step of job. default is 0 for new jobs</param>
        /// <returns></returns>
        public async Task<List<WorkFlowTypesViewModel>> GetNextWorkActionflowStepsAsync(int workFlowId, int workflowTypeId) {

            //get current workflow
            var currWorkflow = await _procurementDBContext.Workflows.FirstOrDefaultAsync(m=>m.Id == workFlowId);

            if (currWorkflow != null)
            {
                //get current workflow type Id
                var wfId = currWorkflow.WorkflowTypeId;
                //get current workflow step
                var step = currWorkflow.Step;

                var workflows = await _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == wfId && x.Step > step).OrderBy(x => x.Step).ToListAsync();

                var nextTwoWorkflows = workflows.Take(1);

                var data = nextTwoWorkflows.Select(x => new WorkFlowTypesViewModel
                {
                    Name = x.WorkflowAction.Name,
                    Id = x.Id
                }).ToList();


                return (data);
                    
            }
            else
            {
                //new jobs e.g new requisition tasks not yet assigned a task
                var workflows = await _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == workflowTypeId).OrderBy(x=> x.Step).ToListAsync();

                var nextTwoWorkflows = workflows.Take(1);

                var data = nextTwoWorkflows.Select(x => new WorkFlowTypesViewModel
                {
                    Name = x.WorkflowAction.Name,
                    Id = x.Id
                }).ToList();

                return data;
            }

           
        }
        
        /// <summary>
        /// Gets the current steps for a workflow
        /// </summary>
        /// <param name="id">current step of job. default is 0 for new jobs</param>
        /// <returns></returns>
        public async Task<List<WorkFlowTypesViewModel>> GetCurrentWorkActionflowStepsAsync(int workFlowId, int workflowTypeId)
        {

            //get current workflow
            var currWorkflow = await _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(m => m.Id == workFlowId).ToListAsync();

            var data = currWorkflow.Select(x => new WorkFlowTypesViewModel
            {
                Name = x.WorkflowAction.Name,
                Id = x.Id
            }).ToList();

            return (data);
        }

        /// <summary>
        /// Gets all future steps for a workflow
        /// </summary>
        /// <param name="workflowTypeId">The workflow type's Id</param>
        /// <returns></returns>
        public async Task<WorkFlowTypesViewModel> GetFirstWorkActionflowStepAsync(int workflowTypeId)
        {
            //new jobs e.g new requisition tasks not yet assigned a task
            var workflows = await _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == workflowTypeId).OrderBy(x => x.Step).ToListAsync();

            var nextTwoWorkflows = workflows.Take(1);

            var data = nextTwoWorkflows.Select(x => new WorkFlowTypesViewModel
            {
                Name = x.WorkflowAction.Name,
                Id = x.Id
            }).FirstOrDefault();

            return data;
        }

        /// <summary>
        /// gets all the steps the job has passed through
        /// </summary>
        /// <param name="currentStepId">current step of job</param>
        /// <returns></returns>
        public async Task<List<WorkFlowTypesViewModel>> GetPreviousWorkActionflowStepsAsync(int workFlowId, int workFlowTypeId)
        {
            //get current workflow
            var currWorkflow = await _procurementDBContext.Workflows.FirstOrDefaultAsync(m => m.Id == workFlowId);

            if (currWorkflow != null)
            {
                //get current workflow type Id
                var wfId = currWorkflow.WorkflowTypeId;
                //get current workflow step
                var step = currWorkflow.Step;

                var workflows = await _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == wfId && x.Step < step).OrderByDescending(x => x.Step).ToListAsync();

                var data = workflows.Select(x => new WorkFlowTypesViewModel
                {
                    Name = x.WorkflowAction.Name,
                    Id = x.Id
                }).ToList();


                return data;

            }

            return new List<WorkFlowTypesViewModel>();
        }
            
    }
}
