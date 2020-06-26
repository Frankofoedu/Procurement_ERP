using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Filters.Attributes;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using BsslProcurement.UtilityMethods;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{

    [DisplayName("New Requisition")]
    [Authorize]
    [NoDiscovery]
    public class NewRequisitionModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly ProcurementDBContext _procContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IRequisitionService _requisitionService;
        private readonly IPRNumberService _pRNumberService;


        public string Message { get; set; }
        public string Error { get; set; }
        public string UserCode { get; set; }
        //public string PrNo { get; set; }
        //public string RequestingDept { get; set; }
        //public string RequestingDeptCode { get; set; }

        //public List<SelectListItem> Departments { get; set; }


        #region Properties
        [BindProperty]
        public bool IsRejected { get; set; }
        [BindProperty]
        public List<ItemGridViewModel> gridVm { get; set; }
        [BindProperty]
        public Requisition Requisition { get; set; }
        [BindProperty]
        public WorkFlowApproverViewModel WfVm { get; set; }
        [BindProperty]
        public string QuarantineRemark { get; set; }
        #endregion

        public NewRequisitionModel(UserManager<User> userManager,
            BSSLSYS_ITF_DEMOContext bsslContext,
            ProcurementDBContext procContext, 
            IRequisitionService requisitionService, 
            IPRNumberService pRNumberService, 
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _bsslContext = bsslContext;
            _procContext = procContext;
            _environment = environment;
            _requisitionService = requisitionService;
            _pRNumberService = pRNumberService;
        }

        public async Task<ActionResult> OnGetAsync(int? id, string message, bool isRejected)
        {
            try
            {
                Message = message;
                IsRejected = isRejected;
                await LoadData();

                if (id != null)
                {
                    Requisition = _procContext.Requisitions.Include(x => x.RequisitionItems).ThenInclude(c => c.Attachment).FirstOrDefault(c => c.Id == id);

                    if (Requisition == null)
                    {
                        Error = "No requisition found";
                        return Page();
                    }

                    gridVm = await LoadGridViewItemsFromRequisition(Requisition, _environment);
                }
            }
            catch (Exception ex)
            {
                Error = $"An error occured. Please contact Support. {ex.Message}";
            }

            return Page();


        }
        private async Task ClearRejectedTasks(int id, string remark)
        {
            var rejectedJob = await _procContext.RequisitionJobs.FirstOrDefaultAsync(x => x.RequisitionId == id && x.JobStatus == Enums.JobState.Rejected);

            if (rejectedJob != null)
            {
                rejectedJob.SetAsDone(DateTime.Now, remark);
            }
        }

        public async Task<ActionResult> OnPostSubmitAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }



            try
            {
                //check if its a saved requisition

                if (id != null)
                {
                    //check if it is a rejected requisition
                    if (IsRejected)
                    {
                        await ClearRejectedTasks(id.Value, WfVm.Remark);
                    }
                    Requisition.UpdateRequisitionCreationDate(DateTime.Now);

                    await SaveOrSubmitRequisition(true, true);
                }
                else
                {
                    //save requisition
                    await SaveOrSubmitRequisition(true, false);
                }

                //dont create initiator job for rejected requisitions
                if (!IsRejected)
                {
                    //create and mark done initiator requisition job
                    await _requisitionService.CreateInitiatorJobAsync(Requisition.Id, (await GetCurrentUserAsync()).Id, WfVm.Remark, false);

                }

                //create and assign next requisition job
                await _requisitionService.SendRequisitionToNextStageAsync(Requisition.Id,
                        WfVm.AssignedStaffCode, WfVm.WorkFlowId, WfVm.Remark);

                return RedirectToPage(new { message = $"Requisition submitted successfully. PR Number = '{Requisition.PRNumber}'" });

            }
            catch (Exception ex)
            {
                Error = "An error has occurred." + Environment.NewLine + ex.Message;
                await LoadData();
                return Page();
            }



        }

        /// <summary>
        /// Deletes a Requisition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> OnPostQuarantineAsync(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != null)
                    {
                        await _requisitionService.SendToQuarantine(id.Value, $"Quarantined - {QuarantineRemark}");

                        Message = "Requisition Quarantined";
                    }
                }
                catch (Exception ex)
                {
                    Error = "An error has occurred." + Environment.NewLine + ex.Message;
                    await LoadData();
                    return Page();
                }

            }

            await LoadData();
            return RedirectToPage(new { message = Message });
        }

        public async Task<ActionResult> OnPostSaveAsync(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id != null)
                    {
                        await SaveOrSubmitRequisition(false, true);
                        Message = "Requisition Updated and Saved successfully";
                        return RedirectToPage(new { message = Message });
                    }
                    else
                    {
                        await SaveOrSubmitRequisition(false, false);

                        return RedirectToPage(new { message = $"Requisition Saved. PR Number = '{Requisition.PRNumber}'" });
                    }
                }
                catch (Exception ex)
                {
                    Error = "An error has occurred." + Environment.NewLine + ex.Message;
                }

            }

            await LoadData();
            return Page();
        }

        private async Task LoadData()
        {
            //get current logged in user
            var curUser = await GetCurrentUserAsync();
            UserCode = curUser.UserName;

            //load requisition workflow
            var workflow = _procContext.WorkflowTypes.Include(c => c.Workflows).FirstOrDefault(x => x.Name == DcProcurement.Constants.RequisitionWorkflow);

            if (workflow.Workflows.Count > 0)
            {
                WfVm = new WorkFlowApproverViewModel { WorkFlowTypeId = workflow.Id };
            }
        }

        private async Task<List<ItemGridViewModel>> LoadGridViewItemsFromRequisition(Requisition requisition, IWebHostEnvironment env)
        {
            FormFile CreateFormFile(string filename)
            {
                var fn = System.IO.Path.Combine(env.WebRootPath, filename);

                using var stream = System.IO.File.OpenRead(fn);
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/pdf"
                };

                return file;
            }

            try
            {

                return requisition.RequisitionItems.Select(x => new ItemGridViewModel { RequisitionItem = x, AttachmentId = x.Attachment != null ? x.Attachment.Id : (int?)null, Attachment = x.Attachment != null ? CreateFormFile(x.Attachment.FilePath) : null }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private async Task SaveOrSubmitRequisition(bool isSubmitted, bool isUpdate)
        {
            //save requisition items
            Requisition.RequisitionItems = await GetRequisitionItemsFromViewModel(gridVm);

            Requisition.RequisitionState = isSubmitted ? Enums.RequisitionState.Submitted : Enums.RequisitionState.Saved;

            Requisition.Status = isSubmitted ? "Sent For Processing" : "Saved";

            Requisition.LoggedInUserId = (await GetCurrentUserAsync()).Id;

            //checks if requisition is an update or new requisition
            if (isUpdate)
            {
                if (Requisition.RequisitionItems.Any())
                {
                    _procContext.RequisitionItems.UpdateRange(Requisition.RequisitionItems);
                    foreach (var item in Requisition.RequisitionItems)
                    {
                        if (item.Attachment != null)
                        {
                            _procContext.Attachments.Update(item.Attachment);
                        }
                    }
                }

                _procContext.Requisitions.Update(Requisition);
            }
            else
            {
                Requisition.PRNumber = await _pRNumberService.GetNewPRNumberAsync(Requisition.PRNumber);

                _procContext.Requisitions.Add(Requisition);

            }

            await _procContext.SaveChangesAsync();

        }

        private async Task<List<RequisitionItem>> GetRequisitionItemsFromViewModel(List<ItemGridViewModel> gridVm)
        {
            List<RequisitionItem> reList = new List<RequisitionItem>();
            foreach (ItemGridViewModel v in gridVm)
            {
                RequisitionItem re = v.RequisitionItem;
                if (!v.isAttachmentChanged)
                {
                    if (v.Attachment != null)
                    {
                        re.Attachment = await FileUpload.GetFilePathsFromFileAsync(v.Attachment, _environment, "Attachment");
                        re.Attachment.Id = v.AttachmentId ?? 0;
                    }
                    else
                    {
                        re.Attachment = null;
                    }
                }
                else
                {
                    re.Attachment = _procContext.Attachments.Find(v.AttachmentId);
                }
                reList.Add(re);
            }
            return reList;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


    }
}