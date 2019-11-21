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
        public async Task<List<StaffLayoutModel>> GetAllStaffInWorkFlow(int workFlowId)
        {
            
            var st =  _procurementDBContext.WorkflowStaffs.Include(p => p.Staff).Where(x => x.WorkflowId == workFlowId);
                

           var slM =  await st.Select(c => new StaffLayoutModel { StaffCode = c.Staff.StaffCode, StaffName = c.Staff.Name, Rank = null }).ToListAsync();

            return slM;
        }

        public Task<List<StaffLayoutModel>> GetAllStaffNoRank()
        {

            return  _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffName = x.Othernames, StaffCode = x.Staffid, Rank = null}).ToListAsync();
        }

        public Task<List<StaffLayoutModel>> GetAllStaffWithRank()
        {
           return  _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffName = x.Othernames, StaffCode = x.Staffid, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToListAsync();

        }

    }
}
