using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
   public class PrequalificationPolicy
    {
        public int Id { get; set; }
        public int NoOfCategory { get; set; }

        [DataType(DataType.Date)]
        public DateTime PrequalificationStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime PrequalificationEndDate { get; set; }
    }
}
