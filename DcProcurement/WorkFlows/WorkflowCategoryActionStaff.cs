using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class WorkflowCategoryActionStaff
    {
        public int Id { get; set; }
        public int WorkflowTypeId { get; set; }
        public int WorkflowActionId { get; set; }
        public string StaffId { get; set; }

        public WorkflowType WorkflowType { get; set; }
        public WorkflowAction WorkflowAction { get; set; }
        public Staff Staff { get; set; }
    }
}
