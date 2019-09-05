using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class RequisitionItem
    {
        public int Id { get; set; }
        public string StoreItemCode { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        /// <summary>
        /// The identity User attached to this company. (User of type vendor)
        /// </summary>
        public string VendorId { get; set; }
        public double UnitPrice { get; set; }

        public string Amount { get { return (Convert.ToInt32(Quantity) * Convert.ToInt32(UnitPrice)).ToString(); } }
        public int? RequisitionId { get; set; }



        public VendorUser Vendor { get; set; }
        public Requisition Requisition { get; set; }
    }
}
