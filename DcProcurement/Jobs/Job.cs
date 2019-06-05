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
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoneDate { get; set; }
        public bool Done { get; set; }

        public string StaffId { get; set; } // if staff field is null then any staff can see the job.


        public Staff Staff { get; set; }
    }
}
