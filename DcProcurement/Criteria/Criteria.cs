using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class Criteria
    {
        public int Id { get; set; }
        public string CriteriaDescription { get; set; }
        public int? MinValue { get; set; }
        public bool isCompulsory { get; set; }
        public bool NeedsDocument { get; set; }
    }
}
