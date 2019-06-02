using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class ProcurementSubcategory
    {
        public int Id { get; set; }
        public string ProcurementSubCategoryCode { get; set; }
        public int? ProcurementCategoryId { get; set; }
        [Required(ErrorMessage = "This Subcategory Name Field is Required")]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SubCategoryCriteria> SubCategoryCriterias { get; set; }
        public virtual ProcurementCategory ProcurementCategory  { get; set; }
        public List<ProcurementItem> ProcurementItems { get; set; }
    }
}
