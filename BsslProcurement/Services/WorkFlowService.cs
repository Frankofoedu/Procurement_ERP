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
        public Task<List<WorkFlowTypesViewModel>> GetNextWorkActionflowSteps(int workFlowId, int workflowTypeId) {

            //get current workflow
            var currWorkflow = _procurementDBContext.Workflows.Find(workFlowId);

            if (currWorkflow != null)
            {
                //get current workflow type Id
                var wfId = currWorkflow.WorkflowTypeId;
                //get current workflow step
                var step = currWorkflow.Step;

                var workflows = _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == wfId && x.Step > step);

                var nextTwoWorkflows = workflows.Take(1);

                var data = nextTwoWorkflows.Select(x => new WorkFlowTypesViewModel
                {
                    Name = x.WorkflowAction.Name,
                    Id = x.Id
                }).ToListAsync();


                return data;
                    
            }
            else
            {
                //new jobs e.g new requisition tasks not yet assigned a task
                var workflows = _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == workflowTypeId);

                var nextTwoWorkflows = workflows.Take(2);

                var data = nextTwoWorkflows.Select(x => new WorkFlowTypesViewModel
                {
                    Name = x.WorkflowAction.Name,
                    Id = x.Id
                }).ToListAsync();

                return data;
            }

           
        }
            


        /// <summary>
        /// gets all the steps the job has passed through
        /// </summary>
        /// <param name="currentStepId">current step of job</param>
        /// <returns></returns>
        public Task<List<WorkFlowTypesViewModel>> GetPreviousWorkActionflowSteps(int workFlowTypeId, int currentStepId) =>
            //gets all workflow actions for the current job stage
            _procurementDBContext.Workflows.Include(y => y.WorkflowAction).Where(x => x.WorkflowTypeId == workFlowTypeId && x.Step < currentStepId).Select(x => new WorkFlowTypesViewModel { Name = x.WorkflowAction.Name, Id = x.Id }).ToListAsync();
    }
}
