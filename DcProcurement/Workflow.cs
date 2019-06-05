using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    class Workflow
    {
        public int Id { get; set; }
        public int Step { get; set; }
        public string Description { get; set; }
        public bool ToPersonOrGroup { get; set; }
        public string UserId { get; set; }

    }
}
