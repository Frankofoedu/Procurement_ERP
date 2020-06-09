using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class RequisitionItem
    {
        public int Id { get; set; }
        public string StoreItemCode { get; set; }
        public string StoreItemDescription { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
        public string SubCategory { get; set; }
        public string SubCategoryCode { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        /// <summary>
        /// From accust table
        /// </summary>
        public string AccustId { get; set; }
        public double UnitPrice { get; set; }

        public string Amount => (Convert.ToInt32(Quantity) * Convert.ToInt32(UnitPrice)).ToString();
        public int? RequisitionId { get; set; }
        public Attachment Attachment { get; set; }


        public Requisition Requisition { get; set; }
    }
}
