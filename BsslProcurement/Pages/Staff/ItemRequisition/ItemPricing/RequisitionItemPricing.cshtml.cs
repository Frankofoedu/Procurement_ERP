using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DcProcurement;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    [Authorize]
    public class RequisitionItemPricingModel : PageModel
    {
        public readonly ProcurementDBContext context;


        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public DcProcurement.Requisition Requisition { get; set; }

        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext bsslContext;
        public RequisitionItemPricingModel(ProcurementDBContext _context, DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext)
        {
            bsslContext = _bsslContext;
            context = _context;
        }

        public void LoadData(int id)
        {

            Requisition = context.Requisitions.Include(x => x.Attachments).Include(y => y.RequisitionItems).Where(k => k.Id == id).FirstOrDefault();

        }
        public void OnGet(int id)
        {
            LoadData(id);
        }


        public void OnPost(int id)
        {
            if (ModelState.IsValid)
            {


            }
            LoadData(id);
        }

        public PartialViewResult OnGetItemPartial()
        {

           // get all vendor
            var items = bsslContext.Stock.ToList();

            return new PartialViewResult
            {
                ViewName = "Modals/_ItemLayout",
                ViewData = new ViewDataDictionary<List<DcProcurement.Contexts.Stock>>(ViewData, items)
            };
        }

        public PartialViewResult OnGetVendorPartial()
        {

            //get all vendor 
            var vendors = context.Vendors.ToList();


            return new PartialViewResult
            {
                ViewName = "_VendorLayoutModal",
                ViewData = new ViewDataDictionary<List<VendorUser>>(ViewData, vendors)
            };
        }
    }
}