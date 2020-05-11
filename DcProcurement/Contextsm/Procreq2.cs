using System;
using System.Collections.Generic;

namespace DcProcurement.Contextsm
{
    public partial class Procreq2
    {
        public int Id { get; set; }
        public string Reqno { get; set; }
        public string Copny { get; set; }
        public string Zcopny { get; set; }
        public string Acperiod { get; set; }
        public string Acyear { get; set; }
        public DateTime? Tdate { get; set; }
        public string Icode { get; set; }
        public string Idescr { get; set; }
        public decimal? Qty { get; set; }
        public string Uofm { get; set; }
        public decimal? Price { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Oamount { get; set; }
        public string Suggsup { get; set; }
        public string Budfav { get; set; }
        public decimal? Bal { get; set; }
        public string Stop { get; set; }
        public decimal Serno { get; set; }
        public string Cancel { get; set; }
        public string Custcode { get; set; }
        public DateTime? Tensdate { get; set; }
        public DateTime? Tenedate { get; set; }
        public string Quoted { get; set; }
        public string Maindept { get; set; }
        public string Dept { get; set; }
        public string Achead { get; set; }
        public string Subhead { get; set; }
        public decimal? Budamt { get; set; }
        public decimal? Suppamt { get; set; }
        public decimal? Revamt { get; set; }
        public decimal? Relamt { get; set; }
        public decimal? Budbalamt { get; set; }
        public decimal? Commtodateamt { get; set; }
        public decimal? Amtviredtodate { get; set; }
        public decimal? Provbalamt { get; set; }
        public decimal? Amtrquest { get; set; }
    }
}
