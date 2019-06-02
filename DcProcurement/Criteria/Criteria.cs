using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class Criteria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Description Field is Compulsory")]
        public string CriteriaDescription { get; set; }
        public int? MinValue { get; set; }
        public bool isCompulsory { get; set; }
        public bool NeedsDocument { get; set; }
    }
}
