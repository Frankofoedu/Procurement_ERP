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
        protected readonly ProcurementDBContext _procurementDBContext;
        public RequisitionService(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }
        public async Task<List<Requisition>> GetRequisitionsForLoggedInUser(string userId)
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.LoggedInUserId == userId && p.isSubmitted == true).ToListAsync();
        }
        public async Task<List<Requisition>> GetSavedRequisitionsForLoggedInUser(string userId)
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.LoggedInUserId == userId && p.isSubmitted == false).ToListAsync();
        }
        public async Task<List<Requisition>> GetBudgetClearedRequisitions()
        {
            return await _procurementDBContext.Requisitions.Include(x => x.RequisitionItems).Where(p => p.isBudgetCleared==true).ToListAsync();
        }

       
        public async Task SendRequisitionToNextStageAsync(Requisition requisition,string staffCode, int newWorkflowId, string remark)
        {
            //get staff identity id
            var staffId = await GetStaffIdFromCodeAsync(staffCode);
            
            //get old job
            var oldReqJobs = await _procurementDBContext.RequisitionJobs.Where(req => req.RequisitionId == requisition.Id && req.JobStatus == Enums.JobState.NotDone).FirstOrDefaultAsync();

            if (oldReqJobs != null)
            {
                //update old requisitions to done
                oldReqJobs.SetAsDone(DateTime.Now);

            }
            //create new job for next stage
            var newReqJob = new RequisitionJob(requisition.Id, staffId, newWorkflowId, remark);

            _procurementDBContext.RequisitionJobs.Add(newReqJob);

           await _procurementDBContext.SaveChangesAsync();

        }

        public async Task SendRequisitionToPreviousStage(Requisition requisition, string currStaffCode, string newStaffCode, int newStage, string remark)
        {
            //get  staff identity id
            var currStaffId = await GetStaffIdFromCodeAsync(currStaffCode);
            var newStaffId = await GetStaffIdFromCodeAsync(newStaffCode);


            //get current job
            var currJob = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.JobStatus == Enums.JobState.NotDone && x.RequisitionId == requisition.Id && x.StaffId == currStaffId);

            if (currJob == null)
            {
                throw new ArgumentNullException("Job does not exist");
            }
            //set current job as cancelled
            currJob.SetAsCancelled(DateTime.Now);


            //create new job for previous stage
            var newReqJob = new RequisitionJob(requisition.Id, newStaffId, newStage, remark);

            _procurementDBContext.Add(newReqJob);

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
            var job = await _procurementDBContext.RequisitionJobs.FirstOrDefaultAsync(x => x.RequisitionId == requisition.Id && x.JobStatus == Enums.JobState.NotDone);

            return new WorkFlowApproverViewModel { Remark = job.Remark, WorkFlowTypeId = DcProcurement.Constants.RequisitionWorkflowId, WorkFlowId  = job.WorkFlowId };
        }
        private async Task<string> GetStaffIdFromCodeAsync(string staffCode) => (await _procurementDBContext.Staffs.FirstOrDefaultAsync(x => x.StaffCode == staffCode)).Id;
    }
}
