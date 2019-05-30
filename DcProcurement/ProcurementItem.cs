using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class ProcurementItem
    {
        public int Id { get; set; }
        public int? ProcurementGroupId { get; set; }
        public int? ProcurementSubcategoryId { get; set; }
        [Required(ErrorMessage = "The item name field is required.")]
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }

        public ICollection<ProcurementCriteria> ProcurementCriterias { get; set; }
        public ProcurementGroup ProcurementGroup { get; set; }
        public ProcurementSubcategory ProcurementSubcategory { get; set; }
    }
}
