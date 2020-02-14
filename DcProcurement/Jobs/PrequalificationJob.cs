using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class PrequalificationJob : Job
    {
        private PrequalificationJob(){}

        public PrequalificationJob(int compId, string staffId, int workflowId, string remark)
        {
            CompanyInfoId = compId;
            CreationDate = DateTime.Now;
            StaffId = staffId;
            Remark = remark;
            WorkFlowId = workflowId;
            JobStatus = Enums.JobState.NotDone;
        }
        public int? CompanyInfoId { get; set; }


        public CompanyInfo CompanyInfo { get; set; }
    }
}
