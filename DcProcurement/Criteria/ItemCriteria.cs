using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class ItemCriteria : Criteria
    {
        public int? ProcurementItemId { get; set; }

        public ProcurementItem  ProcurementItem { get; set; }
    }
}
