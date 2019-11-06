using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement.WorkFlows
{
    class WorkflowCategoryActionStaff
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        public int WorkflowCategoryId { get; set; }
        public int WorkflowActionId { get; set; }
        public string StaffId { get; set; }

        public WorkflowType WorkflowType { get; set; }
        public WorkflowAction WorkflowAction { get; set; }
        public Staff Staff { get; set; }
    }
}
