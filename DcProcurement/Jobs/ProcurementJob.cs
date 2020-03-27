using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement.Jobs
{
    public class ProcurementJob : Job
    {
        private ProcurementJob() { }
        public ProcurementJob(int reqId, string staffId, int workflowId, string remark)
        {
            RequisitionProcId = reqId;
            CreationDate = DateTime.Now;
            StaffId = staffId;
            Remark = remark;
            WorkFlowId = workflowId;
            JobStatus = Enums.JobState.NotDone;
        }
        public int? RequisitionProcId { get; set; }

        [ForeignKey("RequisitionProcId")]
        public Requisition Requisition { get; set; }
    }
}
