using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class CategoryCriteria : Criteria
    {
        public int? ProcurementCategoryId { get; set; }

        public ProcurementCategory ProcurementCategory { get; set; }
    }
}
