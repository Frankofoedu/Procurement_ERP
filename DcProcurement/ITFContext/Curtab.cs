using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Curtab
    {
        public string Curcode { get; set; }
        public string Curname { get; set; }
        public decimal? Raten { get; set; }
        public DateTime Ratedate { get; set; }
        public string Symbol { get; set; }
        public string Exequacct { get; set; }
    }
}
