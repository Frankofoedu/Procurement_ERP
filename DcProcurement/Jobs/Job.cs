using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Job
    {
        public int Id { get; set; }
        /// <summary>
        /// check if there is any workflow in the db before setting. 
        /// if no workflow in the db, set to 0 else set to 1
        /// </summary>
        public int WorkFlowStep { get; set; } //current workflow step

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoneDate { get; set; }
        public bool Done { get; set; }

        public string StaffId { get; set; } // if staff field is null then any staff can see the job.


        public Staff Staff { get; set; }
    }
}
