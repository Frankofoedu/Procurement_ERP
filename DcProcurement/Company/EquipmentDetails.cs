using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class EquipmentDetails
    {
        public int Id { get; set; }
        public int CompanyInfoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
    }
}
