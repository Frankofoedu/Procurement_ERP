using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class ContractSubcategory
    {
        public int Id { get; set; }
        public int? ContractCategoryId { get; set; }
        [Required(ErrorMessage = "This Subcategory Name Field is Required")]
        public string SubcategoryName { get; set; }
        public string SubcategoryDescription { get; set; }

        public ContractCategory ContractCategory { get; set; }
    }
}
