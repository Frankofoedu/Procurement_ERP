using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class SubmittedCriteria
    {
        public int Id { get; set; }
        public int CompanyInfoId { get; set; }
        public int CriteriaId { get; set; }
        public string Value { get; set; }
        public virtual Criteria Criteria { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
    }
}
