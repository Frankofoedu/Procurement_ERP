using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class Requisition
    {
        public int Id { get; set; }
        public string PRNumber { get; set; }
        public bool ProcurementType { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string RequestionDepartment { get; set; }
        public string RequiredAt { get; set; }
        public string PreparedBy { get; set; }
        public string PreparedByRank { get; set; }
        public string PreparedFor { get; set; }
        public string PreparedForRank { get; set; }
        public string Purpose { get; set; }

        public List<RequisitionItem> RequisitionItems { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
