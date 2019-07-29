using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DcProcurement
{
    public class Workflow
    {
        public int Id { get; set; }

        public int Step { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public bool ToPersonOrAssign { get; set; }

        public string StaffId { get; set; }
       public string AlternativeStaffId { get; set; }

        [ForeignKey("StaffId")]
        public Staff StaffToAssign { get; set; }

        [ForeignKey("AlternativeStaffId")]
        public Staff AlternativeStaffToAssign { get; set; }
    }
}
