using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement
{
    public class Workflow
    {
        public int Id { get; set; }
        public int? WorkflowActionId { get; set; }
        public int? WorkflowTypeId { get; set; }

        public int Step { get; set; }


        public WorkflowType WorkflowType { get; set; }
        public WorkflowAction WorkflowAction { get; set; }

        public ICollection<WorkflowStaff> Staffs { get; set; }
    }
}
