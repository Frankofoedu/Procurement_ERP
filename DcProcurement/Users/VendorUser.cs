using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class VendorUser : User
    {
        public int? CompanyInfoId { get; set; }

        public virtual CompanyInfo CompanyInfo { get; set; }

        public ICollection<RequisitionItem> RequisitionItems { get; set; }
    }
}
