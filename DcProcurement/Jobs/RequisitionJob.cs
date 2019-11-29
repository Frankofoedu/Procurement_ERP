using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement.Jobs
{
    public class RequisitionJob: Job
    {
        public int RequisitionId { get; set; }
        public Requisition Requisition { get; set; }
    }
}
