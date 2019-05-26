using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class ContractCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name Field is Required")]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public ICollection<ContractSubcategory> ContractSubcategories { get; set; }
    }
}
