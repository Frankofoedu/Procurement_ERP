using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public int? ProcurementSubcategoryId { get; set; }
        [Required(ErrorMessage = "The item name field is required.")]
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual ICollection<ProcurementCriteria> ProcurementCriterias { get; set; }
        public ProcurementSubcategory ProcurementSubcategory { get; set; }
    }
}
