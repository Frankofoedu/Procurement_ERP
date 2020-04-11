using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class WorkflowStaff
    {
        public int Id { get; set; }
        public int WorkflowId { get; set; }
        public string StaffId { get; set; }
        public Enums.WorkflowStaffState State { get; set; } = Enums.WorkflowStaffState.Normal;

        public Workflow Workflow { get; set; }
        public Staff Staff { get; set; }
    }
}
