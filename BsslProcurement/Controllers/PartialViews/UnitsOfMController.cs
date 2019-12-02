using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Controllers.PartialViews
{

    [Route("[controller]/[action]")]
    public class UnitsOfMController : Controller
    {
        private BSSLSYS_ITF_DEMOContext bsslContext;
        public UnitsOfMController(BSSLSYS_ITF_DEMOContext _bsslContext)
        {
            bsslContext = _bsslContext;
        }
        public PartialViewResult GetUOM()
        {
            //get all vendor 
            var uomList = bsslContext.UnitOfMeasurements.ToList();

            //var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_UOMLayout",
                ViewData = new ViewDataDictionary<List<UnitOfMeasurement>>(ViewData, uomList)
            };
        }
    }
}