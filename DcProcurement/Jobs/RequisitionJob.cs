using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement.Jobs
{
    public class RequisitionJob: Job
    {
        private RequisitionJob() { }

        public RequisitionJob(int reqId, string staffId,int workflowId, string remark) 
        {
            RequisitionId = reqId;
            CreationDate = DateTime.Now;
            StaffId = staffId;
            Remark = remark;
            WorkFlowId = workflowId;
            JobStatus = Enums.JobState.NotDone;
        }
        public int RequisitionId { get; private set; }
        public Requisition Requisition { get; private set; }

      
    }
}
