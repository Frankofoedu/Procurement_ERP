using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.ViewModels
{
    public class ProcurementJobViewModel
    {
        public Requisition Requisition { get; set; }

        public int? WorkflowAction { get; set; }
    }
}
