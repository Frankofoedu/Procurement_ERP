using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
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
        private readonly ProcurementDBContext _procurementDBContext;
        public RequisitionService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }

        public async Task<List<RequisitionJob>> GetRequisitionsJobsAssignedToLoggedInUser(string userId)
        {
            var jobs = _procurementDBContext.RequisitionJobs
                .Include(req=> req.Workflow).ThenInclude(wk => wk.WorkflowAction)
                .Include(r=> r.Requisition).ThenInclude(ri=>ri.RequisitionItems)
                .Where(x => x.StaffId == userId && ( x.JobStatus == Enums.JobState.NotDone || x.JobStatus == Enums.JobState.Rejected));

            if (jobs != null)
            {
                return await jobs.ToListAsync();
            }

            return null;
        }
        public async Task<List<Requisition>> GetRequisitionsForLoggedInUser(string userId)
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.LoggedInUserId == userId).ToListAsync();
        }
        public async Task<List<Requisition>> GetSavedRequisitionsForLoggedInUser(string userId)
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).ThenInclude(x=> x.Attachment).Where(p => p.LoggedInUserId == userId && p.RequisitionState == Enums.RequisitionState.Saved).OrderBy(x => x.DateCreated).ToListAsync();
        }


        public async Task SendRequisitionToNextStageAsync(int requisitionId,string staffCode, int newWorkflowId, string remark)
        {
            //get staff identity id
            string staffId = "";
            if (staffCode != null)
            {

                staffId = await GetStaffIdFromCodeAsync(staffCode);
            }
            
            //get old job
            var oldReqJob = await _procurementDBContext.RequisitionJobs.Where(req => req.RequisitionId == requisitionId && req.JobStatus == Enums.JobState.NotDone).FirstOrDefaultAsync();

            //get requisition workflow stages
            var reqWorkFlow = _procurementDBContext.Workflows.Where(x => x.WorkflowTypeId == DcProcurement.Constants.RequisitionWorkflowId).OrderBy(x => x.Step);

            
            if (oldReqJob != null)
            {
                //update old requisitions jobs to done
                oldReqJob.SetAsDone(DateTime.Now, remark);

                //checks if job is at final stage for that requistion
                var maxWorkFlow = reqWorkFlow.Last();
                if (oldReqJob.WorkFlowId == maxWorkFlow.Id)
                {
                    //if at final stage, set requisition as approved

                    var requisition = _procurementDBContext.Requisitions.Find(requisitionId);

                    if (requisition != null)
                    {
                        requisition.RequisitionState = Enums.RequisitionState.Approved;
                    }
                    
                }
                else
                {
                    //send to next step
                    //create new job for next stage
                    var newReqJob = new RequisitionJob(requisitionId, staffId, newWorkflowId);
                    _procurementDBContext.RequisitionJobs.Add(newReqJob);
                }

            }
            else
            {
                //create new job for next stage
                var newReqJob = new RequisitionJob(requisitionId, staffId, newWorkflowId);
                _procurementDBContext.RequisitionJobs.Add(newReqJob);
            }

            await _procurementDBContext.SaveChangesAsync();

        }

        //Create initiator job
        public async Task CreateInitiatorJobAsync(int requisitionId, string staffId, string remark, bool isRejected)
        {
            //get Initiator workflow
            var InitiatorWorkflow = await _procurementDBContext.Workflows.Include(m=>m.WorkflowAction).Include(n=>n.WorkflowType)
                .FirstOrDefaultAsync(IW => IW.WorkflowType.Name == Constants.RequisitionWorkflow && IW.WorkflowAction.Name == Constants.InitiatorActionName);

            var newReq = new RequisitionJob(requisitionId, staffId, InitiatorWorkflow.Id);
            if (isRejected)
            {
                newReq.SetAsRejected(DateTime.Now, remark);
            }
            else
            {
                newReq.SetAsDone(DateTime.Now, remark);
            }
            

            _procurementDBContext.RequisitionJobs.Add(newReq);

            await _procurementDBContext.SaveChangesAsync();
        }

        //TODO: Update this method in line with the nextstage method
        public async Task SendRequisitionToPreviousStage(int requisitionId, string currStaffCode, string newStaffCode, int newStage, string remark)
        {
            //get  staff identity id
            var currStaffId = await GetStaffIdFromCodeAsync(currStaffCode);
            var newStaffId = await GetStaffIdFromCodeAsync(newStaffCode);


            //get current job
            var currJob = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.JobStatus == Enums.JobState.NotDone && x.RequisitionId == requisitionId && x.StaffId == currStaffId);

            if (currJob == null)
            {
                throw new ArgumentNullException("Job does not exist");
            }
            //set current job as cancelled
            currJob.SetAsCancelled(DateTime.Now, remark);


            //create new job for previous stage
            var newReqJob = new RequisitionJob(requisitionId, newStaffId, newStage);

            _procurementDBContext.Add(newReqJob);

            await _procurementDBContext.SaveChangesAsync();
        }
        public async Task RejectRequisition(int requisitionId, string rejectionRemark)
        {
            //get current job
            var currJob = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.JobStatus == Enums.JobState.NotDone && x.RequisitionId == requisitionId);

            if (currJob == null)
            {
                throw new ArgumentNullException("Job does not exist");
            }
            //set current job as done
            currJob.SetAsDone(DateTime.Now, rejectionRemark);

            //send requisition back to initiator
            //get initiator from requisition
            var staffId = (await _procurementDBContext.Requisitions.FirstOrDefaultAsync(x => x.Id == requisitionId))?.LoggedInUserId;

            if (staffId != null)
            {
                //create rejected job for initiator
                await CreateInitiatorJobAsync(requisitionId, staffId, "", true);

            }

        }
        public async Task SendToQuarantine(int requisitionId, string remark)
        {
            //get current job
            var currJob = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.JobStatus == Enums.JobState.NotDone && x.RequisitionId == requisitionId);

            if (currJob == null)
            {
                throw new ArgumentNullException("Job does not exist");
            }

            //set current job as done
            currJob.SetAsDone(DateTime.Now, remark);

            var req = await _procurementDBContext.Requisitions.FirstOrDefaultAsync(m => m.Id == requisitionId);
            req.RequisitionState = Enums.RequisitionState.Quarantined;
            req.Status = "Quarantined";

            await _procurementDBContext.SaveChangesAsync();
        }

        public async Task ReAssignRequisition(Requisition requisition,string currStaffCode, string newStaffCode)
        {
            //get staff id from code
            var currStaffId = await GetStaffIdFromCodeAsync(currStaffCode);
            var newStaffId = await GetStaffIdFromCodeAsync(newStaffCode);

            //get current job
            var currJob = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.JobStatus == Enums.JobState.NotDone && x.RequisitionId == requisition.Id && x.StaffId == currStaffId);

            if (currJob == null)
            {
                throw new ArgumentNullException("Job does not exist");
            }

            currJob.ReAssignJob(newStaffId);

            await _procurementDBContext.SaveChangesAsync();
        }

        public async Task<WorkFlowApproverViewModel> GetCurrentWorkFlowOFRequisition(Requisition requisition)
        {
            var job = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.RequisitionId == requisition.Id && (x.JobStatus == Enums.JobState.NotDone || x.JobStatus == Enums.JobState.Rejected));

            var staffCode = await GetStaffCodeFromIdAsync(job.StaffId);

            return new WorkFlowApproverViewModel { Remark = job.Remark, AssignedStaffCode = staffCode, WorkFlowTypeId = DcProcurement.Constants.RequisitionWorkflowId, WorkFlowId  = job.WorkFlowId };
        }
        public async Task<List<Requisition>> GetSubmittedRequisitionsForLoggedInUser(string userId)
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.LoggedInUserId == userId && p.RequisitionState== Enums.RequisitionState.Submitted).ToListAsync();

        }
        public async Task<List<Requisition>> GetApprovedRequisitionsForLoggedInUser(string userId)
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.LoggedInUserId == userId && p.RequisitionState == Enums.RequisitionState.Approved).ToListAsync();

        }


        private async Task<string> GetStaffIdFromCodeAsync(string staffCode) => (await _procurementDBContext.Staffs.FirstOrDefaultAsync(x => x.StaffCode == staffCode)).Id;
        private async Task<string> GetStaffCodeFromIdAsync(string staffId) => (await _procurementDBContext.Staffs.FindAsync(staffId)).Id;

      
    }
}
