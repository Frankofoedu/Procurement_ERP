using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement
{
    public class Job
    {
        public int Id { get; private set; }
        /// <summary>
        /// check if there is any workflow in the db before setting. 
        /// if no workflow in the db, set to 0 else set to 1
        /// </summary>
        public int WorkFlowStep { get; protected set; } //current workflow step

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; protected set; }
        [DataType(DataType.Date)]
        public DateTime? DoneDate { get; protected set; }
        public Enums.JobState JobStatus { get; protected set; }

        public string  Remark { get; protected set; }
        public string StaffId { get; protected set; } // if staff field is null then any staff can see the job.

        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        public void SetAsDone(DateTime doneDate)
        {
            JobStatus = Enums.JobState.Done;
            DoneDate = doneDate;
        }
    }
}
