using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Useracct
    {
        public decimal Pkvalue { get; set; }
        public string Userid { get; set; }
        public string Pwd { get; set; }
        public string Grpcode { get; set; }
        public string Username { get; set; }
        public bool? Suspend { get; set; }
        public bool? Loggedon { get; set; }
        public bool? Deleted { get; set; }
        public bool? Raccess { get; set; }
        public bool? Uaccess { get; set; }
        public string Compcode { get; set; }
        public bool? Newlogin { get; set; }
        public DateTime? Expiredate { get; set; }
        public bool? Nvrexpire { get; set; }
        public bool? Rptaccess { get; set; }
        public bool? Prtaccess { get; set; }
        public bool? Admin { get; set; }
        public bool? Gledger { get; set; }
        public bool? AcctPay { get; set; }
        public bool? AcctRec { get; set; }
        public bool? FixdAsst { get; set; }
        public bool? AcctSetup { get; set; }
        public bool? Budget { get; set; }
        public bool? StockInvent { get; set; }
        public bool? Analysis { get; set; }
        public bool? Revenue { get; set; }
        public bool? BankCash { get; set; }
        public bool? GovBudget { get; set; }
        public bool? Project { get; set; }
        public bool? Addybook { get; set; }
        public bool? Audit { get; set; }
        public bool? Warrant { get; set; }
        public bool? StockReq { get; set; }
        public bool? Coordination { get; set; }
        public bool? Procurement { get; set; }
        public string Accslvl { get; set; }
        public string Zcopny { get; set; }
        public DateTime? Modidate { get; set; }
        public DateTime? Datein { get; set; }
        public bool? Patsalinvo { get; set; }
        public DateTime? Lastlogin { get; set; }
    }
}
