using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Staff : User
    {
        [Display(Name = "Fullname")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Minimum of 3 and Maximum of 255 characters.")]
        [Required(ErrorMessage = "Account Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Staff Code Field is required.")]
        public string StaffCode { get; set; }
        [Required(ErrorMessage = "Position Field is required.")]
        public string Position { get; set; }

        public ICollection<PrequalificationJob> AssignedPrequalificationJobs { get; set; }

        public ICollection<Workflow>  StaffWorkflows { get; set; }
        public ICollection<Workflow> AdditionalStaffWorkflows { get; set; }

    }
}
