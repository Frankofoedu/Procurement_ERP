using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
   public class PersonnelDetails
    {
        public int Id { get; set; }
        public int? CompanyInfoId { get; set; }
        public string    Name { get; set; }
        public string Qualification { get; set; }
        public string CV { get; set; }
        public string Certificate { get; set; }
        public string Passport { get; set; }
    }
}
