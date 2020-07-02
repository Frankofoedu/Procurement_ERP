using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Controllers.PartialViews
{

    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ItemsController : Controller
    {

        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext bsslContext;
        public ItemsController(BSSLSYS_ITF_DEMOContext _bsslContext)
        {
            bsslContext = _bsslContext;
        }

        public PartialViewResult GetItemPartial()
        {

            // get all vendor
            var items = bsslContext.Stock.ToList();

            return new PartialViewResult
            {
                ViewName = "Modals/_ItemLayout",
                ViewData = new ViewDataDictionary<List<DcProcurement.Contexts.Stock>>(ViewData, items.Distinct( new StockComparer()).ToList())
            };
        }
    }

    // Custom comparer for the Product class
    class StockComparer : IEqualityComparer<Stock>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Stock x, Stock y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.TypeCode == y.TypeCode && x.Groupno == y.Groupno && x.Stockno == y.Stockno;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Stock stock)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(stock, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashStockTypeCode = stock.TypeCode == null ? 0 : stock.TypeCode.GetHashCode();
            //Get hash code for the Name field if it is not null.
            int hashStockGroupno = stock.Groupno == null ? 0 : stock.Groupno.GetHashCode();
            //Get hash code for the Name field if it is not null.
            int hashStockStockno = stock.Stockno == null ? 0 : stock.Groupno.GetHashCode();

            //Calculate the hash code for the product.
            return hashStockTypeCode ^ hashStockGroupno ^ hashStockStockno;
        }
    }
}
