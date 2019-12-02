using BsslProcurement.Interfaces;
using DcProcurement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class RequisitionService : IRequisitionService
    {
        protected readonly ProcurementDBContext _procurementDBContext;
        public RequisitionService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }
        public Task<List<Requisition>> GetRequisitionsForLoggedInUser(string userId)
        {
            return _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.LoggedInUserId == userId).ToListAsync();
        }
    }
}
