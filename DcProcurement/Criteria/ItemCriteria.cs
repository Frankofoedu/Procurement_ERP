using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    public class ItemCriteria : Criteria
    {
        public int? ItemId { get; set; }

        public Item  Item { get; set; }
    }
}
