using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Item Code field is required.")]
        public string ItemCode { get; set; }
        [Required(ErrorMessage = "Please select subcategory.")]
        public int? ProcurementSubcategoryId { get; set; }
        [Required(ErrorMessage = "The item name field is required.")]
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime? DateAdded { get; set; }

        public ICollection<ItemCriteria> ItemCriterias { get; set; }
        public ProcurementSubcategory ProcurementSubcategory { get; set; }
    }
}
