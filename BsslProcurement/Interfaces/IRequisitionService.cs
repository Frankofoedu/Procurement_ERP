using BsslProcurement.ViewModels;
using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IRequisitionService
    {
        Task<List<Requisition>> GetRequisitionsAssignedToLoggedInUser(string userId);
        Task<List<Requisition>> GetRequisitionsForLoggedInUser(string userId);
        Task<List<Requisition>> GetSavedRequisitionsForLoggedInUser(string userId);
        Task<List<Requisition>> GetBudgetClearedRequisitions();
        Task<WorkFlowApproverViewModel> GetCurrentWorkFlowOFRequisition(Requisition requisition);

        Task SendRequisitionToNextStageAsync(Requisition requisition, string staffCode, int newStage, string remark);
        Task SendRequisitionToPreviousStage(Requisition requisition, string currStaffCode, string newStaffCode, int newStage, string remark);
        Task ReAssignRequisition(Requisition requisition, string currStaffCode, string newStaffCode);
       
    }
}
