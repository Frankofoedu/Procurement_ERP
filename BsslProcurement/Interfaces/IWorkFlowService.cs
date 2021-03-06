﻿using BsslProcurement.ViewModels;
using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IWorkFlowService
    {

        Task<List<WorkFlowTypesViewModel>> GetNextWorkActionflowStepsAsync(int workFlowId, int workflowTypeId);

        Task<List<WorkFlowTypesViewModel>> GetCurrentWorkActionflowStepsAsync(int workFlowId, int workflowTypeId);

        Task<List<WorkFlowTypesViewModel>> GetPreviousWorkActionflowStepsAsync(int requisitionId);

        Task<WorkFlowTypesViewModel> GetFirstWorkActionflowStepAsync(int workflowTypeId);

        Task<List<Workflow>> GetRequisitionWorkflows();



        //Task<WorkFlowStaffViewModel> GetMainDepartmentHead();
        //Task<WorkFlowStaffViewModel> GetDepartmentHead();
        //Task<WorkFlowStaffViewModel> GetSectionHead();
        //Task<WorkFlowStaffViewModel> GetUnitHead();
    }
}
