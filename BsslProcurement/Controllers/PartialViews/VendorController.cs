using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Controllers.PartialViews
{
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class VendorController : Controller
    {
        private ProcurementDBContext Context;
        public VendorController(ProcurementDBContext _Context)
        {
            Context = _Context;
        }

        public PartialViewResult GetVendorWithEmail()
        {
            //get all vendor 
            var vendorList = Context.Vendors.Include(m=>m.CompanyInfo).ToList();

            //var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_VendorWithEmailPartial",
                ViewData = new ViewDataDictionary<List<VendorUser>>(ViewData, vendorList)
            };
        }
    }
}