using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;

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
        public Task<WorkFlowStaffViewModel> GetNextWorkflowSteps(int id = 0)
        {
            throw new NotImplementedException();
            // _procurementDBContext.WorkflowActions.Where(x => x.)
        }

        /// <summary>
        /// gets all the steps the job has passed through
        /// </summary>
        /// <param name="id">current step of job</param>
        /// <returns></returns>
        public Task<WorkFlowStaffViewModel> GetPreviousflowSteps(int id)
        {
            throw new NotImplementedException();
        }
    }
}
