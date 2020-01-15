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
    }
}
