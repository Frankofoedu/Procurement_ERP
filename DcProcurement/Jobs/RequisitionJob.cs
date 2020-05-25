using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? RequisitionId { get; private set; }
        [ForeignKey("RequisitionId")]
        public Requisition Requisition { get; private set; }

        public static RequisitionJob Empty()
        {
            return new RequisitionJob();
        }
    }
}
