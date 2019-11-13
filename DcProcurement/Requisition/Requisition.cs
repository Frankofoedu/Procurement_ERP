using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Requisition
    {
        public int Id { get; set; }
        public bool isSubmitted { get; set; } = false;

        [Required(ErrorMessage ="Please provide the description for this Requisition")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Requisition Number is required")]
        public string PRNumber { get; set; }
        public string ProcurementType { get; set; }
        [Required(ErrorMessage ="Select Date")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Select Estimated Delivery Date")]
        public DateTime? DeliveryDate { get; set; }
        [Required(ErrorMessage = "Select Requester Type")]
        public string RequesterType { get; set; }
        [Required(ErrorMessage = "Select Requester")]
        public string RequesterValue { get; set; }
        [Required(ErrorMessage = "Select Department Required At")]
        public string RequiredAtDepartment { get; set; }
        [Required(ErrorMessage = "Select Staff Prepared By")]
        public string PreparedBy { get; set; }
        public string PreparedByRank { get; set; }
        [Required(ErrorMessage = "Select Staff Prepared For")]
        public string PreparedFor { get; set; }
        public string PreparedForRank { get; set; }
        public string Purpose { get; set; }
        public string ProcurementMethod { get; set; }
        public string ProcessType { get; set; }
        public string ERFx { get; set; }
        //== true when requisition items has been priced
        public bool isPriced { get; set; }
        public string Status { get; set; }

        //default database creation date
        public DateTime DateCreated { get; set; }
        public List<RequisitionItem> RequisitionItems { get; set; }
       // public List<Attachment> Attachments { get; set; }
    }
}
