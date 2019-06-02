using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class ProcurementItem
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public int? ProcurementGroupId { get; set; }
        public string Info { get; set; }
        public DateTime? DateAdded { get; set; }

        public ProcurementGroup ProcurementGroup { get; set; }
        public Item Item { get; set; }
    }
}
