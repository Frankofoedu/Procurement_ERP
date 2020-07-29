using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Actax
    {
        public string Taxcode { get; set; }
        public string Taxdesc { get; set; }
        public string Taxac { get; set; }
        public decimal? Taxrate { get; set; }
        public DateTime? Intrdate { get; set; }
        public string Paidto { get; set; }
        public decimal? Taxtype { get; set; }
        public string Copny { get; set; }
        public string Accountid { get; set; }
        public string Bankid { get; set; }
        public string Bankdescr { get; set; }
    }
}
