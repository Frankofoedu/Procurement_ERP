using BsslProcurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public interface IWorkFlowService
    {
     
        Task<WorkFlowStaffViewModel> GetNextWorkflowSteps(int id = 0);

        /// <summary>
        /// gets all the steps the job has passed through
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WorkFlowStaffViewModel> GetPreviousflowSteps(int id);

        //Task<WorkFlowStaffViewModel> GetMainDepartmentHead();
        //Task<WorkFlowStaffViewModel> GetDepartmentHead();
        //Task<WorkFlowStaffViewModel> GetSectionHead();
        //Task<WorkFlowStaffViewModel> GetUnitHead();
    }
}
