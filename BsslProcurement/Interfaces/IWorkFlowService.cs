using BsslProcurement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IWorkFlowService
    {

        Task<List<WorkFlowTypesViewModel>> GetNextWorkActionflowSteps(int workFlowTypeId, int currentStepId = 0);

        Task<List<WorkFlowTypesViewModel>> GetPreviousWorkActionflowSteps(int workFlowTypeId, int currentStepId);



        //Task<WorkFlowStaffViewModel> GetMainDepartmentHead();
        //Task<WorkFlowStaffViewModel> GetDepartmentHead();
        //Task<WorkFlowStaffViewModel> GetSectionHead();
        //Task<WorkFlowStaffViewModel> GetUnitHead();
    }
}
