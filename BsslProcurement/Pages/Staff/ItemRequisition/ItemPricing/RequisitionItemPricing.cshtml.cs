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
using BsslProcurement.ViewModels;
using DcProcurement.Contexts;
using BsslProcurement.Interfaces;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    [Authorize]
    public class RequisitionItemPricingModel : PageModel
    {
        public readonly ProcurementDBContext context;
        private readonly IRequisitionService _requisitionService;

        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public DcProcurement.Requisition Requisition { get; set; }

        [BindProperty]
        public VendorWithEmailViewModel VendorEmailListObj { get; set; }
        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }

        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext bsslContext;
        public RequisitionItemPricingModel(ProcurementDBContext _context, DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext, IRequisitionService requisitionService)
        {
            bsslContext = _bsslContext;
            context = _context;
            _requisitionService = requisitionService;
        }

        public async Task LoadDataAsync(int id)
        {

            //load workflow of requisition
            WfVm = await _requisitionService.GetCurrentWorkFlowOFRequisition(Requisition);



            VendorEmailListObj = new VendorWithEmailViewModel();

            Requisition = context.Requisitions.Include(y => y.RequisitionItems).FirstOrDefault(k => k.Id == id);
            VendorEmailListObj.VendorWithEmailList = VendorEmailListObj.GetVendorWithEmailList(bsslContext.Accusts.ToList());
        }
        public void OnGet(int id)
        {
            LoadDataAsync(id);
        }


        public void OnPost(int id)
        {
            if (ModelState.IsValid)
            {

            }
            LoadDataAsync(id);
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

       
    }
}