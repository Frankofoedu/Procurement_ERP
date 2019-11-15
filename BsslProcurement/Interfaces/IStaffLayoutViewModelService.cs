using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IStaffLayoutViewModelService
    {
        void GetAllStaffWithRank();
        Task<List<WorkflowStaff>> GetAllStaffInWorkFlow(int workFlowId);

    }
}
