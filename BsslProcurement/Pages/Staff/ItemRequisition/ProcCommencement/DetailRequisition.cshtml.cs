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

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcCommencement
{
    public class DetailRequisitionModel : PageModel
    {

        private readonly ProcurementDBContext _context;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly IItemGridViewModelService _itemGridViewModelService;
        private readonly IProcurementService _procurementService;


        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }
        public DetailRequisitionModel(ProcurementDBContext context, BSSLSYS_ITF_DEMOContext bsslContext, IItemGridViewModelService itemGridViewModelService, IProcurementService procurementService)
        {
            _context = context;
            _bsslContext = bsslContext;
            _itemGridViewModelService = itemGridViewModelService;
            _procurementService = procurementService;
        }
        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public Requisition Requisition { get; set; }
        public List<ItemGridViewModel> ItemGridViewModels { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


           await LoadData(id);

            return Page();
        }
        public async Task<ActionResult> OnPostSubmitAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //update requisition


                    //create and assign requisition job
                    await _procurementService.SendRequisitionToNextStageAsync(Requisition.Id, WfVm.AssignedStaffCode, WfVm.WorkFlowId, WfVm.Remark);


                    return RedirectToPage("AllRequisition");
                }
                catch (Exception ex)
                {
                    Error = "An error has occurred." + Environment.NewLine + ex.Message;
                }
            }

           // await LoadData();
            return Page();
        }

        private async Task LoadData(int? id)
        {

            Requisition = await _context.Requisitions.FirstOrDefaultAsync(x => x.Id == id);
            ItemGridViewModels = await _itemGridViewModelService.GetItemsInRequisition(id.Value);
            //   ItemGridViewModels = Requisition.RequisitionItems.Select(x=> new ItemGridViewModel { Attachment = x.Attachment, RequisitionItem = x });

            if (Requisition == null)
            {
                Error = "No requisition Found";
                return;
            }

            //load procurement workflow
            var workflow = _context.WorkflowTypes.Include(c => c.Workflows).FirstOrDefault(x => x.Name == DcProcurement.Constants.ProcurementWorkflow);

            if (workflow.Workflows.Count() > 0)
            {
                WfVm = new WorkFlowApproverViewModel { WorkFlowTypeId = workflow.Id };
            }
           
        }



        public PartialViewResult OnGetStaffPartial()
        {

            //get all staff and thier ranks
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffName = x.Othernames, StaffCode = x.Staffid, Rank = null }).ToList();


            //           var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }
    }
}