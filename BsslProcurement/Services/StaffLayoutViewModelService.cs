using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class StaffLayoutViewModelService : IStaffLayoutViewModelService
    {

        private readonly ProcurementDBContext _procurementDBContext;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        public StaffLayoutViewModelService(ProcurementDBContext procurementDBContext, BSSLSYS_ITF_DEMOContext bsslContext)
        {
            _procurementDBContext = procurementDBContext;
            _bsslContext = bsslContext;
        }
        public async Task<List<StaffLayoutModel>> GetAllStaffInWorkFlow(int workFlowId) => 
            (await _procurementDBContext.Workflows.Include(p => p.Staffs).Where(x => x.Id == workFlowId).FirstOrDefaultAsync()).Staffs.Select(c => new StaffLayoutModel { StaffCode = c.Staff.StaffCode, StaffName = c.Staff.UserName, Rank= null }).ToList();

        public Task<List<StaffLayoutModel>> GetAllStaffNoRank()
        {
            throw new NotImplementedException();
        }

        public async Task<List<StaffLayoutModel>> GetAllStaffWithRank()
        {
           return await _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffName = x.Othernames, StaffCode = x.Staffid, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToListAsync();

        }

    }
}
