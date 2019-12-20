using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    public class DetailRequisitionModel : PageModel
    {

        private readonly ProcurementDBContext _context;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly IItemGridViewModelService _itemGridViewModelService;
        private readonly IRequisitionService requisitionService;
        public DetailRequisitionModel(ProcurementDBContext context, BSSLSYS_ITF_DEMOContext bsslContext, IItemGridViewModelService itemGridViewModelService, IRequisitionService _requisitionService)
        {
            _context = context;
            _bsslContext = bsslContext;
            _itemGridViewModelService = itemGridViewModelService;
            requisitionService = _requisitionService;
        }
        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public Requisition Requisition { get; set; }
        [BindProperty]
        public List<ItemGridViewModel> ItemGridViewModels { get; set; }
        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Requisition = await _context.Requisitions.FirstOrDefaultAsync(x=> x.Id == id);
            
         //   ItemGridViewModels = Requisition.RequisitionItems.Select(x=> new ItemGridViewModel { Attachment = x.Attachment, RequisitionItem = x });

            if (Requisition == null)
            {
                Error = "No requisition Found";
                return Page();
            }
            ItemGridViewModels = await _itemGridViewModelService.GetItemsInRequisition(id.Value);

            WfVm = await requisitionService.GetCurrentWorkFlowOFRequisition(Requisition);

            return Page();
        }


      
    }
}