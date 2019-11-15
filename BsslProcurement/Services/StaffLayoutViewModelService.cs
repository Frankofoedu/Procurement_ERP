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
        public StaffLayoutViewModelService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }
        public async Task<List<WorkflowStaff>> GetAllStaffInWorkFlow(int workFlowId) => 
            (await _procurementDBContext.Workflows.Include(p => p.Staffs).Where(x => x.Id == workFlowId).FirstOrDefaultAsync()).Staffs.ToList();
       
        
        public void GetAllStaffRank()
        {
           // var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { Staff = x, Rank = null }).ToList();

        }

        public void GetAllStaffWithRank()
        {
           // var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { Staff = x, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToList();
            

        }
    }
}
