using DcProcurement;
using DcProcurement.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
    public interface IJobService
    {
        Task<List<RequisitionJob>> GetApprovalJobsForRequisitionsAsync(List<Requisition> requisitions);
        Task<List<RequisitionJob>> GetLastJobsForRequisitionsAsync(List<Requisition> requisitions);
    }
}
