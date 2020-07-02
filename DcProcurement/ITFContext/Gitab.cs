using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Gitab
    {
        public int Serno { get; set; }
        public string Itemid { get; set; }
        public string Suppcode { get; set; }
        public string Orderno { get; set; }
        public string Orderloccode { get; set; }
        public string Ordertype { get; set; }
        public string Initiator { get; set; }
        public string VType { get; set; }
        public string VNumber { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Acctstore { get; set; }
        public string Acctsupplier { get; set; }
        public string Tos { get; set; }
        public string Groupno { get; set; }
        public string Stockno { get; set; }
        public string Descr { get; set; }
        public string Uofm { get; set; }
        public string Warehouse { get; set; }
        public decimal? Orderqty { get; set; }
        public decimal? Qtysupplied { get; set; }
        public string Bincardno { get; set; }
        public decimal? Price { get; set; }
        public decimal? Unitprice { get; set; }
        public decimal? Issuedqty { get; set; }
        public string Preqno { get; set; }
        public string Pbno { get; set; }
        public DateTime? Pbdate { get; set; }
        public string Pdetail { get; set; }
        public string Pac { get; set; }
        public string Sdetail2 { get; set; }
        public string Gdetail2 { get; set; }
        public string Glfolio { get; set; }
        public string Inby { get; set; }
        public string Srv { get; set; }
        public string Isrv { get; set; }
        public decimal? Sqn { get; set; }
        public string Copny { get; set; }
        public string Zcopny { get; set; }
        public string Acperiod { get; set; }
        public string Acyear { get; set; }
        public DateTime? Datein { get; set; }
        public string Srvused { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Oldqty { get; set; }
        public decimal? Discount { get; set; }
    }
}
