using BsslProcurement.ViewModels;
using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IProcurementService
    {
        Task SendRequisitionToNextStageAsync(int requisitionId, string staffCode, int newStage, string remark);
        Task SendRequisitionToPreviousStage(int requisitionId, string currStaffCode, string newStaffCode, int newStage, string remark);
        Task<List<ProcurementJobViewModel>> GetProcurementRequisitionsJobsAssignedToLoggedInUser(string id);

        Task<List<Requisition>> GetRequisitionsForPricing();
        Task<List<Requisition>> GetRequisitionsForPricingAssignedToUser(string userId);

        Task<List<Requisition>> GetApprovedRequisitions();

        Task<WorkFlowApproverViewModel> GetCurrentWorkFlowOFRequisition(Requisition requisition);
    }
}
