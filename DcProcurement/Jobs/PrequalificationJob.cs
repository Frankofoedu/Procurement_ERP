using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class PrequalificationJob : Job
    {
        public int CompanyInfoId { get; set; }


        public CompanyInfo CompanyInfo { get; set; }
    }
}
