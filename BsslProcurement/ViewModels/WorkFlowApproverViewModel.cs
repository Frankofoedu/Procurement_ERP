using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.ViewModels
{
    public class WorkFlowApproverViewModel
    {
        public int WorkFlowId { get; set; }
        public string AssignedStaffCode { get; set; }
        public string SelectedAction { get; set; }
        public string AssignedStaffName { get; set; }
        public int? WorkFlowTypeId { get; set; }
        public int WorkflowStep { get; set; }
        public string Remark { get; set; }
    }
}
