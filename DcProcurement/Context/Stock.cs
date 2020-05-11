using System;
using System.Collections.Generic;

namespace DcProcurement.Contexts
{
    public partial class Stock
    {
        public string TypeCode { get; set; }
        public string Groupno { get; set; }
        public string Stockno { get; set; }
        public string WCode { get; set; }
        public string WBinFrom { get; set; }
        public string WBinTo { get; set; }
        public string Itemdesc { get; set; }
        public string Units { get; set; }
        public decimal? ReorderLevel { get; set; }
        public decimal? QtyInStock { get; set; }
        public decimal? Uprice { get; set; }
        public string StockStat { get; set; }
        public decimal? MinQty { get; set; }
        public decimal? Salesprice { get; set; }
        public decimal? Valprice { get; set; }
        public string Binlocat { get; set; }
        public string Valmethod { get; set; }
        public string Details { get; set; }
        public string Partno { get; set; }
        public string Placcount { get; set; }
        public decimal? Vatrate { get; set; }
        public string Salesacct { get; set; }
        public string Cosalesact { get; set; }
        public string Perichitem { get; set; }
        public DateTime? Expdate { get; set; }
        public string Assetcode { get; set; }
        public string Acctcode { get; set; }
        public decimal? Reorderqty { get; set; }
        public decimal? Lastcqty { get; set; }
        public DateTime? Lastcdate { get; set; }
        public decimal? Lastcprice { get; set; }
        public string Copny { get; set; }
        public string Zcopny { get; set; }
        public string Inuse { get; set; }
        public DateTime? Inusetime { get; set; }
        public string Userid { get; set; }
        public string Surdefacct { get; set; }
        public string Project2 { get; set; }
        public string Nonestock { get; set; }
        public int Id { get; set; }
    }
}
