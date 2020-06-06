using DcProcurement.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Requisition
    {
        public bool isDeleted { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage ="Please provide the description for this Requisition")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Requisition Number is required")]
        public string PRNumber { get; set; }
        public string ProcurementType { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public DateTime? Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Select Estimated Delivery Date")]
        public DateTime? DeliveryDate { get; set; }
        [Required(ErrorMessage = "Select Requester Type")]
        public string RequesterType { get; set; }
        [Required(ErrorMessage = "Select Requester")]
        public string RequesterCode { get; set; }
        [Required(ErrorMessage = "Select Requester")]
        public string RequesterValue { get; set; }
        [Required(ErrorMessage = "Select Department Required At")]
        public string RequiredAtDepartment { get; set; }
        [Required(ErrorMessage = "Select Staff Prepared By")]
        public string PreparedBy { get; set; }
        public string PreparedByRank { get; set; }
        public string PreparedForType { get; set; }
        [Required(ErrorMessage = "Select Staff Prepared For")]
        public string PreparedFor { get; set; }
        public string PreparedForRank { get; set; }
        public string Purpose { get; set; }
        public string ProcurementMethod { get; set; }
        public string ProcessType { get; set; }
        public string ERFx { get; set; }

        public Enums.RequisitionState RequisitionState { get; set; } = Enums.RequisitionState.Saved;
        public Enums.ProcurementState ProcurementState { get; set; } = Enums.ProcurementState.NotStarted;

        public string Status { get; set; }
        public string LoggedInUserId { get; set; }

        //default database creation date
        public DateTime? DateCreated { get; private set; } = DateTime.Now;
        public List<RequisitionItem> RequisitionItems { get; set; }
         public List<RequisitionJob> RequisitionJobs { get; set; }
        public List<ProcurementJob> ProcurementJobs { get; set; }


        public virtual ERFXSetup ERFXSetup { get; set; }

        public void UpdateRequisitionCreationDate(DateTime dateCreated)
        {
            DateCreated = dateCreated;
        }
    }
}
