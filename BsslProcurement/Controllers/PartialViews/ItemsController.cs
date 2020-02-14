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
                ViewData = new ViewDataDictionary<List<DcProcurement.Contexts.Stock>>(ViewData, items)
            };
        }
    }
}
