using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Controllers.PartialViews
{
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class VendorController : Controller
    {
        private readonly BSSLSYS_ITF_DEMOContext bsslContext;
        private ProcurementDBContext Context;
        public VendorController(BSSLSYS_ITF_DEMOContext _bsslContext, ProcurementDBContext _Context)
        {
            Context = _Context;
            bsslContext = _bsslContext;
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

        public PartialViewResult GetSupplierPartial()
        {
            //get all vendor 
            var vendorList = bsslContext.Accusts.ToList();

            //var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_SupplierPartial",
                ViewData = new ViewDataDictionary<List<Accust>>(ViewData, vendorList)
            };
        }
    }
}