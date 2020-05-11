using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Codestab
    {
        public decimal Codeid { get; set; }
        public string Code { get; set; }
        public string Desc1 { get; set; }
        public string Option1 { get; set; }
        public string Compcode { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Barr { get; set; }
        public string Appto { get; set; }
        public string Grade { get; set; }
        public string Desc2 { get; set; }
        public string Step { get; set; }
        public string Desc3 { get; set; }
        public bool? Consolidate { get; set; }
        public bool? Suspend { get; set; }
        public string Rowno { get; set; }
        public string Orderby { get; set; }
        public int? Orderno { get; set; }
        public string Shtcode { get; set; }
        public string Prefixcode { get; set; }
        public string Appto1 { get; set; }
        public string Memoaddr { get; set; }
    }
}
