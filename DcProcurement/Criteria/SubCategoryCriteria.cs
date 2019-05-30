using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
   public class SubCategoryCriteria : Criteria
    {
        public int? ProcurementSubcategoryId { get; set; }

        public ProcurementSubcategory ProcurementSubcategory { get; set; }
    }
}
