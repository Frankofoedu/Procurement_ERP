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
            var oldReqJob = await _procurementDBContext.ProcurementJobs.Where(req => req.RequisitionProcId == requisitionId && req.JobStatus == Enums.JobState.NotDone).FirstOrDefaultAsync();

            //get requisition workflow stages
            var reqWorkFlow = _procurementDBContext.Workflows.Where(x => x.WorkflowTypeId == DcProcurement.Constants.ProcurementWorkflowId).OrderBy(x => x.Step);


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
                    var newReqJob = new ProcurementJob(requisitionId, staffId, newWorkflowId);
                    _procurementDBContext.ProcurementJobs.Add(newReqJob);
                }

            }
            else
            {

                //create new job for next stage
                var newReqJob = new ProcurementJob(requisitionId, staffId, newWorkflowId);
                _procurementDBContext.ProcurementJobs.Add(newReqJob);
            }

            await _procurementDBContext.SaveChangesAsync();
        }



        public async Task<List<Requisition>> GetApprovedRequisitions()
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.RequisitionState == Enums.RequisitionState.Approved).ToListAsync();
        }

        public async Task<List<Requisition>> GetRequisitionsForPricing()
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.isPriced == false && p.RequisitionState == Enums.RequisitionState.Approved).ToListAsync();
        }

        public Task SendRequisitionToPreviousStage(int requisitionId, string currStaffCode, string newStaffCode, int newStage, string remark)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProcurementJobViewModel>> GetProcurementRequisitionsJobsAssignedToLoggedInUser(string userId)
        {
            var jobs = _procurementDBContext.ProcurementJobs.Include(reqJob => reqJob.Requisition).ThenInclude(req => req.RequisitionItems).Include(x=> x.Workflow).Where(x => x.StaffId == userId && x.JobStatus == Enums.JobState.NotDone);

            if (jobs != null)
            {
                var reqList = new List<ProcurementJobViewModel>();

                reqList = await jobs.Select(x => new ProcurementJobViewModel { Requisition = x.Requisition, WorkflowAction = x.Workflow.WorkflowActionId }).ToListAsync();
                return reqList;
            }

            return null;
        }

        public async Task<List<Requisition>> GetRequisitionsForPricingAssignedToUser(string userId)
        {
            var t = _procurementDBContext.ProcurementJobs.Include(procJob => procJob.Requisition).ToList();

            var jobs = _procurementDBContext.ProcurementJobs.Include(procJob => procJob.Requisition).Include(x => x.Workflow).Where(x => x.StaffId == userId && x.JobStatus == Enums.JobState.NotDone );

            if (jobs != null)
            {
                return await jobs.Select(x => x.Requisition).ToListAsync();
            }

            return null;
        }

        public async Task<WorkFlowApproverViewModel> GetCurrentWorkFlowOFRequisition(Requisition requisition)
        {
            var job = await _procurementDBContext.ProcurementJobs.FirstOrDefaultAsync(x => x.RequisitionProcId == requisition.Id && x.JobStatus == Enums.JobState.NotDone);

            var staffCode = await GetStaffCodeFromIdAsync(job.StaffId);

            return new WorkFlowApproverViewModel { Remark = job.Remark, AssignedStaffCode = staffCode, WorkFlowTypeId = DcProcurement.Constants.RequisitionWorkflowId, WorkFlowId = job.WorkFlowId };
        }
    }
}
