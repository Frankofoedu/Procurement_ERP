using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class ProcurementCategory
    {
        public int Id { get; set; }
        public string ProcurementCategoryCode { get; set; }
        [Required(ErrorMessage = "The Name Field is Required")]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CategoryCriteria> CategoryCriterias { get; set; }
        public virtual ICollection<ProcurementSubcategory>  ProcurementSubcategories { get; set; }
    }
}
