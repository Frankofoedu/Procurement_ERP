using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Requisition
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Requisition Number is required")]
        public string PRNumber { get; set; }
        [Required(ErrorMessage = "Select Type of procuremnet")]
        public string ProcurementType { get; set; }
        [Required(ErrorMessage ="Select Date")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public DateTime? DeliveryDate { get; set; }
        [Required(ErrorMessage = "Select Department Requesting")]
        public string RequestionDepartment { get; set; }
        [Required(ErrorMessage = "Select Department Required At")]
        public string RequiredAtDepartment { get; set; }
        [Required(ErrorMessage = "Select Staff Prepared By")]
        public string PreparedBy { get; set; }
        public string PreparedByRank { get; set; }
        [Required(ErrorMessage = "Select Staff Prepared For")]
        public string PreparedFor { get; set; }
        public string PreparedForRank { get; set; }
        public string Purpose { get; set; }
        public List<RequisitionItem> RequisitionItems { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
