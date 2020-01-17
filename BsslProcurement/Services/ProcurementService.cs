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
    public class ProcurementService : IProcurementService
    {
        private readonly ProcurementDBContext _procurementDBContext;
        public ProcurementService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }

        private async Task<string> GetStaffCodeFromIdAsync(string staffId) => (await _procurementDBContext.Staffs.FindAsync(staffId)).Id;
        private async Task<string> GetStaffIdFromCodeAsync(string staffCode) => (await _procurementDBContext.Staffs.FirstOrDefaultAsync(x => x.StaffCode == staffCode)).Id;
        public async Task SendRequisitionToNextStageAsync(int requisitionId, string staffCode, int newWorkflowId, string remark)
        {
            //get staff identity id
            string staffId = "";
            if (staffCode != null)
            {

                staffId = await GetStaffIdFromCodeAsync(staffCode);
            }

            //get old job
            var oldReqJob = await _procurementDBContext.ProcurementJobs.Where(req => req.RequisitionId == requisitionId && req.JobStatus == Enums.JobState.NotDone).FirstOrDefaultAsync();

            //get requisition workflow stages
            var reqWorkFlow = _procurementDBContext.Workflows.Where(x => x.WorkflowTypeId == DcProcurement.Constants.ProcurementWorkflowId).OrderBy(x => x.Step);


            if (oldReqJob != null)
            {
                //update old requisitions jobs to done
                oldReqJob.SetAsDone(DateTime.Now);

                //checks if job is at final stage for that requistion
                var maxWorkFlow = reqWorkFlow.Last();
                if (oldReqJob.WorkFlowId == maxWorkFlow.Id)
                {
                    //if at final stage, set requisition as approved

                    var requisition = _procurementDBContext.Requisitions.Find(requisitionId);

                    if (requisition != null)
                    {
                        requisition.isApproved = true;
                    }

                }
                else
                {
                    //send to next step
                    //create new job for next stage
                    var newReqJob = new ProcurementJob(requisitionId, staffId, newWorkflowId, remark);
                    _procurementDBContext.ProcurementJobs.Add(newReqJob);
                }

            }
            else
            {

                //create new job for next stage
                var newReqJob = new ProcurementJob(requisitionId, staffId, newWorkflowId, remark);
                _procurementDBContext.ProcurementJobs.Add(newReqJob);
            }

            await _procurementDBContext.SaveChangesAsync();
        }

        public Task SendRequisitionToPreviousStage(int requisitionId, string currStaffCode, string newStaffCode, int newStage, string remark)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Requisition>> GetProcurementRequisitionsJobsAssignedToLoggedInUser(string userId)
        {
            var jobs = _procurementDBContext.ProcurementJobs.Include(reqJob => reqJob.Requisition).ThenInclude(req => req.RequisitionItems).Where(x => x.StaffId == userId && x.JobStatus == Enums.JobState.NotDone);

            if (jobs != null)
            {
                var reqList = new List<Requisition>();

                reqList = await jobs.Select(x => x.Requisition).ToListAsync();
                return reqList;
            }

            return null;
        }
    }
}
