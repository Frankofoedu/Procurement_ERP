using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Compdata
    {
        public string Compcode { get; set; }
        public string Names { get; set; }
        public string Cname { get; set; }
        public string Systemname { get; set; }
        public string Caddr { get; set; }
        public string Caddr2 { get; set; }
        public string Caddr3 { get; set; }
        public string Acyear { get; set; }
        public string Acyear2 { get; set; }
        public decimal? Clclosed { get; set; }
        public DateTime? Closedate { get; set; }
        public decimal? Curperiod { get; set; }
        public decimal? Lastdepp { get; set; }
        public string Accode { get; set; }
        public decimal? Lastupd { get; set; }
        public string Intsusp { get; set; }
        public string Rsasset { get; set; }
        public string Assetcurrent { get; set; }
        public string Assetscrcu { get; set; }
        public string Depcurrent { get; set; }
        public DateTime? Tindexdate { get; set; }
        public decimal? Deplimit { get; set; }
        public string Complogo { get; set; }
        public string Coytype { get; set; }
        public string Paytax { get; set; }
        public string Capvat { get; set; }
        public string Allocation { get; set; }
        public string Reporthead { get; set; }
        public string Comptype { get; set; }
        public byte[] Compylogo { get; set; }
        public string Faxtel { get; set; }
        public string Email { get; set; }
        public string Webaddr { get; set; }
    }
}
