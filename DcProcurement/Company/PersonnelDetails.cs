using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
   public class PersonnelDetails
    {
        public int Id { get; set; }
        public int? CompanyInfoId { get; set; }
        public string Name { get; set; }
        public string Qualification { get; set; }
     
        public Attachment CV { get; set; }
        public Attachment Certificate { get; set; }
        public Attachment Passport { get; set; }
        public Enums.VerificationStates VerificationState { get; set; }

        public CompanyInfo CompanyInfo { get; set; }
    }
}
