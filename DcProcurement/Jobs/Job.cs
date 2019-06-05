using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Job
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAssigned { get; set; }

        public string StaffId { get; set; }


        public Staff Staff { get; set; }
    }
}
