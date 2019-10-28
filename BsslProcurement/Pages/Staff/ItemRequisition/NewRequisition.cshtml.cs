using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.UtilityMethods;
using DcProcurement;
using DcProcurement.Contexts;
using DcProcurement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{


    [Authorize]
    public class NewRequisitionModel : PageModel
    {
        private readonly UserManager<DcProcurement.User> _userManager;
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly DcProcurement.ProcurementDBContext _procContext;
        private IHostingEnvironment _environment;

        public class ItemGridViewModel
        {
            public RequisitionItem RequisitionItem { get; set; }
            public  IFormFile Attachment { get; set; }
        }

        [BindProperty]
        public string serialNo { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public string PrNo { get; set; }

        [BindProperty]
        public List<ItemGridViewModel> gridVm { get; set; }

        //[BindProperty]
        //public IFormFile File { get; set; }

        [BindProperty]
        public string RequestingDept { get; set; }

        [BindProperty]
        public string RequestingDeptCode { get; set; }

        [BindProperty]
        public List<SelectListItem> Departments { get; set; }

        [BindProperty]
        public Requisition Requisition { get; set; }

        public NewRequisitionModel(UserManager<DcProcurement.User> userManager,
            BSSLSYS_ITF_DEMOContext bsslContext,
            DcProcurement.ProcurementDBContext procContext, IHostingEnvironment environment)
        {
            _userManager = userManager;
            _bsslContext = bsslContext;
            _procContext = procContext;
            _environment = environment;
        }
        public async Task OnGetAsync()
        {

            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                Error = $"An error occured. Please contact Support. {ex.Message}";
            }

        }

        public async Task<ActionResult> OnPostSubmitAsync(List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var filePaths = (await FileUpload.GetFilePathsAsync(files, _environment, "Attachments"));

                   // Requisition.Attachments = filePaths;
                    Requisition.RequisitionItems = gridVm.Select(x => x.RequisitionItem).ToList();
                    Requisition.isSubmitted = true;
                    
                    _procContext.Requisitions.Add(Requisition);
                    _procContext.PRNos.Add(new PRNo { RequisitionCode = Requisition.PRNumber, LastUsedSerialNo = serialNo });

                    _procContext.SaveChanges();

                    Message = "Requisition Added successfully";
                    return Page();
                }
                catch (Exception ex)
                {
                    Error = "An error has occurred." + Environment.NewLine + ex.Message;
                }

            }
            await LoadData();
            return Page();
        }

        public async Task<ActionResult> OnPostSaveAsync(List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var filePaths = (await FileUpload.GetFilePathsAsync(files, _environment, "Attachments"));

                   // Requisition.Attachments = filePaths;
                    Requisition.RequisitionItems = gridVm.Select(x=> x.RequisitionItem).ToList();
                    Requisition.isSubmitted = true;

                    _procContext.Requisitions.Add(Requisition);
                    _procContext.PRNos.Add(new PRNo { RequisitionCode = Requisition.PRNumber, LastUsedSerialNo = serialNo });

                    _procContext.SaveChanges();

                    Message = "Requisition Saved For Later";
                    return Page();
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
            (PrNo, RequestingDeptCode, RequestingDept, Departments) = await GeneratePRNo();
        }

        public PartialViewResult OnGetStaffPartial()
        {

            //get all staff and thier ranks
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { Staff = x, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToList();


            //           var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }

        public PartialViewResult OnGetVendorPartial()
        {

            //get all vendor 
            var vendors = _procContext.Vendors.ToList();


            //           var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "_VendorLayoutModal",
                ViewData = new ViewDataDictionary<List<VendorUser>>(ViewData, vendors)
            };
        }

        public class requesterObj
        {
            public string code { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }

        public PartialViewResult OnGetRequesterPartial(string type)
        {
            //get requester

            var requesters = new List<requesterObj>();

            if (type.ToLower() == "department")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "F5")
                    .Select(m=> new requesterObj() { code = m.Code, name=m.Desc1, type=type }).ToList();
            }
            else if (type.ToLower() == "division")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "Div")
                    .Select(m => new requesterObj() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "section")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "SECT")
                    .Select(m => new requesterObj() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "unit")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "Z16")
                    .Select(m => new requesterObj() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "staff")
            {
                requesters = _bsslContext.Stafftab.Select(x => new requesterObj { code = x.Staffid, name = x.Surname + x.Othernames, type = type }).ToList();
            }

            return new PartialViewResult
            {
                ViewName = "Modals/_RequesterPartial",
                ViewData = new ViewDataDictionary<List<requesterObj>>(ViewData, requesters)
            };
        }

        private async Task<(string, string, string, List<SelectListItem>)> GeneratePRNo()
        {
            //get current logged in user
            var u = await GetCurrentUserAsync();

            //get staff object
            var user = _procContext.Staffs.Find(u.Id);


            //get current user details from staff table. From the fucking staff table
            var staff = _bsslContext.Useracct.FirstOrDefault(x => x.Userid == user.StaffCode);

            if (staff == null) throw new Exception("Staff Not Setup");
            //get company prefix 
            var companyCode = staff.Compcode;
            var comp = _bsslContext.Compdata.FirstOrDefault(m => m.Compcode == companyCode);

            if (comp == null) throw new Exception("No Company Prefix found");

            var compPrefix = comp.Names;

            var deptCode = _bsslContext.Stafftab.FirstOrDefault(st => st.Staffid == staff.Userid).Deptcode;
            


            var Depts = _bsslContext.Codestab.Where(opt => opt.Option1 == "F5");

            var Dept = Depts.FirstOrDefault(cd => cd.Code == deptCode);

            var DeptPrefix = Dept.Prefixcode;

            var year = DateTime.Now.Year.ToString();

            //implement serial no
            var PrNo = _procContext.PRNos.OrderByDescending(t => t.LastUsedSerialNo).FirstOrDefault();

            serialNo = "";
            serialNo = PrNo == null ? "00001" : (Convert.ToInt32(PrNo.LastUsedSerialNo) + 1).ToString("00000");

            //itf/deptcode/deptprefix/year/serial no

            return ($"{compPrefix.Trim()}/{deptCode.Trim()}/{DeptPrefix.Trim()}/{year}/{serialNo}", deptCode, Dept.Desc1, Depts.Select(depts => new SelectListItem { Text = depts.Desc1 }).ToList());


        }

        private Task<DcProcurement.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        
    }
}