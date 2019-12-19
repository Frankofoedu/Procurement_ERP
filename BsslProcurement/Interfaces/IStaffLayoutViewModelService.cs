using BsslProcurement.ViewModels;
using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IStaffLayoutViewModelService
    {
        Task<List<StaffLayoutModel>> GetAllStaffWithRank();
        Task<List<StaffLayoutModel>> GetAllStaffNoRank();
        Task<List<StaffLayoutModel>> GetAllStaffInWorkFlow(int workFlowId);
        Task<string> GetStaffRank(string staffCode);

    }
}
