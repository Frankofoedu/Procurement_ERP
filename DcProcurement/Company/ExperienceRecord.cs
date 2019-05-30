using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class ExperienceRecord
    {
        public int Id { get; set; }
        public int CompanyInfoId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
    }
}
