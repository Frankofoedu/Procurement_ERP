using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement
{
    public abstract class Job
    {
        public int Id { get; private set; }
        /// <summary>
        /// check if there is any workflow in the db before setting. 
        /// if no workflow in the db, set to 0 else set to 1
        /// </summary>
        public int WorkFlowId { get; protected set; } 

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; protected set; }
        [DataType(DataType.Date)]
        public DateTime? DoneDate { get; protected set; }
        public Enums.JobState JobStatus { get; protected set; }
        public string  Remark { get; protected set; }
        public string StaffId { get; protected set; } // if staff field is null then any staff can see the job.
        [ForeignKey("StaffId")]
        public Staff Staff { get; private set; }

        public Workflow Workflow { get; private set; }



        public void SetAsDone(DateTime doneDate, string remark)
        {
            Remark = remark;
            JobStatus = Enums.JobState.Done;
            DoneDate = doneDate;
        }

        public void SetAsCancelled(DateTime doneDate, string remark)
        {
            Remark = remark;
            JobStatus = Enums.JobState.Cancelled;
            DoneDate = doneDate;
        }

        public void ReAssignJob(string newStaffId)
        {
            StaffId = newStaffId;
        }
    }
}
