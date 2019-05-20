using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement
{
    class ContractSubcategory
    {
        public int Id { get; set; }
        public int? ContractCategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string SubcategoryDescription { get; set; }

    }
}
