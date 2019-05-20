using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    class ProcurementCriteria
    {
        public int Id { get; set; }
        public int? ProcurementItemId { get; set; }
        public string CriteriaDescription { get; set; }
        public int? MinValue { get; set; }
        public bool? isCompulsory { get; set; }
        public bool? NeedsDocument { get; set; }

        public ProcurementItem procurementItem { get; set; }
    }
}
