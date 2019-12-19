using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Jobs;
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
        public Task<List<Requisition>> GetBudgetClearedRequisitions()
        {
            return _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.isBudgetCleared==true).ToListAsync();
        }

        public async Task SendRequisitionToNextStageAsync(Requisition requisition,string staffId, int newStage, string remark)
        {
            //mark old job as done

            //get job
            var oldReqJobs = await _procurementDBContext.RequisitionJobs.Where(req => req.RequisitionId == requisition.Id && req.JobStatus == Enums.JobState.NotDone).FirstOrDefaultAsync();

            if (oldReqJobs != null)
            {
                //update old requisitions to done
                oldReqJobs.SetAsDone(DateTime.Now);
            }

            //create new job for next stage
            var newReqJob = new RequisitionJob(requisition.Id, staffId, newStage, remark);

            _procurementDBContext.Add(newReqJob);

            _procurementDBContext.SaveChanges();

        }

        public Task SendRequisitionToPreviousStage()
        {
            throw new NotImplementedException();
        }

        public Task ReAssignRequisition()
        {
            throw new NotImplementedException();
        }

    }
}
