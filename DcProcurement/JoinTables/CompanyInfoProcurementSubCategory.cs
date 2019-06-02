using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement.JoinTables
{
   public class CompanyInfoProcurementSubCategory
    {
        public int CompanyInfoId { get; set; }
        public CompanyInfo  CompanyInfo { get; set; }

        public int ProcurementSubcategoryId { get; set; }
        public ProcurementSubcategory  ProcurementSubcategory { get; set; }
    }
}
