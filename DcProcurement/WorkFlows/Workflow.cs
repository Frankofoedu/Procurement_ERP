using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class Workflow
    {
        public int Id { get; set; }
        public int Step { get; set; }
        public string Description { get; set; }
        public bool ToPersonOrAssign { get; set; }

        public string StaffId { get; set; }


        public Staff StaffToAssign { get; set; }
    }
}
