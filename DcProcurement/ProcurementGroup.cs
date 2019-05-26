using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class ProcurementGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int NoOfCategory { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }

        public ICollection<ProcurementItem> ProcurementItems { get; set; }
    }
}
