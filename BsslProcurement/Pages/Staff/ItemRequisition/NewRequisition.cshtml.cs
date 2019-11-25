using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace BsslProcurement.Pages.Staff.ItemRequisition
{


    [Authorize]
    public class NewRequisitionModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly ProcurementDBContext _procContext;
        private readonly IWebHostEnvironment _environment;


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
        public List<SelectListItem> UnitsOfMeasurementList { get; set; }
        public List<WorkFlowApproverViewModel> WfVm { get; set; }
        public NewRequisitionModel(UserManager<User> userManager,
            BSSLSYS_ITF_DEMOContext bsslContext,
            ProcurementDBContext procContext, IWebHostEnvironment environment)
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

        public async Task<ActionResult> OnPostSubmitAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await SaveOrSubmitData(true);  
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

        public async Task<ActionResult> OnPostSaveAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await SaveOrSubmitData(false);
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
            UnitsOfMeasurementList = _bsslContext.UnitOfMeasurements.Select(x => new SelectListItem { Text= x.Uname, Value = x.Ucode }).ToList();
            (PrNo, RequestingDeptCode, RequestingDept, Departments) = await GeneratePRNo();
        }

        private async Task SaveOrSubmitData(bool isSubmitted)
        {
            //save requisition items
            Requisition.RequisitionItems = await GetRequisitionItemsFromViewModel(gridVm);   
                       
            Requisition.isSubmitted = isSubmitted;

            _procContext.Requisitions.Add(Requisition);
            _procContext.PRNos.Add(new PRNo { RequisitionCode = Requisition.PRNumber, LastUsedSerialNo = serialNo });

            _procContext.SaveChanges();
        }

        private async Task<List<RequisitionItem>> GetRequisitionItemsFromViewModel(List<ItemGridViewModel> gridVm)
        {
            List<RequisitionItem> reList = new List<RequisitionItem>();
            foreach (ItemGridViewModel v in gridVm)
            {
                RequisitionItem re = v.RequisitionItem;
                 re.Attachment = await FileUpload.GetFilePathsFromFileAsync(v.Attachment,_environment,"Attachment");
                reList.Add(re);

            }
            return reList;
        }

        public PartialViewResult OnGetStaffPartial()
        {

            //get all staff and thier ranks
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { StaffName = x.Othernames,StaffCode = x.Staffid, Rank = _bsslContext.Codestab.FirstOrDefault(m => m.Option1 == "f4" && m.Code == x.Positionid).Desc1 }).ToList();


            //var  = _bsslContext.Stafftab.ToList();
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

        public PartialViewResult OnGetApproverPartial()
        {
            return new PartialViewResult
            {
                ViewName = "Modals/_SelectApproverPartial",
                ViewData = new ViewDataDictionary<List<int>>(ViewData, new List<int>() { 1, 2 })
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
        public PartialViewResult OnGetUOMPartial()
        {

            //get all vendor 
            var uomList = _bsslContext.UnitOfMeasurements.ToList();


            //           var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_UOMLayout",
                ViewData = new ViewDataDictionary<List<UnitOfMeasurement>>(ViewData, uomList)
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