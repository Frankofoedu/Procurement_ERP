using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcCommencement
{
    public class ProcCommencementViewModel
    {
        [Required( ErrorMessage = "Please select Procurement Method")]
        public string ProcMethod { get; set; }
        [Required(ErrorMessage = "Please select Procurement Type")]
        public string ProcType { get; set; }
        [Required(ErrorMessage = "Please select Erfx Type")]
        public string Erfx { get; set; }
        [Required(ErrorMessage = "Please select Procurement Type")]
        public string ProcurementType { get; set; }

    }
    public class DetailRequisitionModel : PageModel
    {

        private readonly ProcurementDBContext _context;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly IItemGridViewModelService _itemGridViewModelService;
        private readonly IProcurementService _procurementService;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public int ReqId { get; set; }
        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }
        [BindProperty]
        public ProcCommencementViewModel Vm { get; set; }
        [BindProperty]
        public List<ItemGridViewModel> ItemGridViewModels { get; set; }


        public DetailRequisitionModel(UserManager<User> userManager, 
            ProcurementDBContext context, BSSLSYS_ITF_DEMOContext bsslContext, 
            IItemGridViewModelService itemGridViewModelService, IProcurementService procurementService)
        {
            _context = context;
            _bsslContext = bsslContext;
            _userManager = userManager;
            _itemGridViewModelService = itemGridViewModelService;
            _procurementService = procurementService;
        }
        public string Message { get; set; }
        public string Error { get; set; }
        public Requisition Requisition { get; set; }
        public List<SelectListItem> CategoryList { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           await LoadData(id);

            return Page();
        }
        public async Task<IActionResult> OnPostSubmitAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //update requisition
                    //get requisition
                    var req = await _context.Requisitions.Include(n=>n.RequisitionItems).FirstOrDefaultAsync(m=>m.Id == ReqId);
                    req.ProcessType = Vm.ProcType;
                    req.ProcurementMethod = Vm.ProcMethod;
                    req.ERFx = Vm.Erfx;
                    req.ProcurementType = Vm.ProcurementType;

                    req.ProcurementState = Enums.ProcurementState.Started;

                    foreach (var item in ItemGridViewModels)
                    {
                        var reqItem = req.RequisitionItems.FirstOrDefault(m => m.Id == item.RequisitionItem.Id);

                        if (reqItem!=null)
                        {
                            reqItem.StoreItemCode = item.RequisitionItem.StoreItemCode;
                            reqItem.StoreItemDescription = item.RequisitionItem.StoreItemDescription;
                            reqItem.Category = item.RequisitionItem.Category;
                            reqItem.CategoryCode = item.RequisitionItem.CategoryCode;
                            reqItem.SubCategory = item.RequisitionItem.SubCategory;
                            reqItem.SubCategoryCode = item.RequisitionItem.SubCategoryCode;
                        }
                    }
                    await _context.SaveChangesAsync();

                    //create and mark done initiator procurement job
                    await _procurementService.CreateInitiatorJobAsync(ReqId, (await GetCurrentUserAsync()).Id, WfVm.Remark);

                    //create and assign procurement job
                    await _procurementService.SendRequisitionToNextStageAsync(ReqId, WfVm.AssignedStaffCode, WfVm.WorkFlowId, WfVm.Remark);


                    return RedirectToPage("AllRequisition");
                }
                catch (Exception ex)
                {
                    Error = "An error has occurred." + Environment.NewLine + ex.Message;
                }

            }

             await LoadData(ReqId);
            return Page();
        }

        private async Task LoadData(int? id)
        {

            ReqId = id.Value;
            Requisition = await _context.Requisitions.FirstOrDefaultAsync(x => x.Id == id);
            ItemGridViewModels = await _itemGridViewModelService.GetItemsInRequisition(id.Value);
            //   ItemGridViewModels = Requisition.RequisitionItems.Select(x=> new ItemGridViewModel { Attachment = x.Attachment, RequisitionItem = x });
            CategoryList = await _bsslContext.Category.Select(cat => new SelectListItem { Value = cat.Code, Text=cat.Descr }).ToListAsync();

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

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


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