using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.UtilityMethods;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    public class AddRequisitionModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly ProcurementDBContext _procContext;
        private readonly IHostingEnvironment _environment;


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
        public AddRequisitionModel(UserManager<User> userManager,
            BSSLSYS_ITF_DEMOContext bsslContext,
            ProcurementDBContext procContext, IHostingEnvironment environment)
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

        private async Task LoadData()
        {
            UnitsOfMeasurementList = _bsslContext.UnitOfMeasurements.Select(x => new SelectListItem { Text = x.Uname, Value = x.Ucode }).ToList();
            (PrNo, RequestingDeptCode, RequestingDept, Departments) = await GeneratePRNo();
        }

        public async Task<ActionResult> OnPostAsync()
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

        private async Task SaveOrSubmitData(bool isSubmitted)
        {
            //save requisition items
            Requisition.RequisitionItems = await GetRequisitionItemsFromViewModel(gridVm);

            Requisition.isSubmitted = isSubmitted;

            _procContext.Requisitions.Add(Requisition);
            _procContext.PRNos.Add(new PRNo { RequisitionCode = Requisition.PRNumber, LastUsedSerialNo = serialNo });

            _procContext.SaveChanges();
        }

        public class requesterObj
        {
            public string code { get; set; }
            public string name { get; set; }
            public string type { get; set; }
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

        private async Task<List<RequisitionItem>> GetRequisitionItemsFromViewModel(List<ItemGridViewModel> gridVm)
        {
            List<RequisitionItem> reList = new List<RequisitionItem>();
            foreach (ItemGridViewModel v in gridVm)
            {
                RequisitionItem re = v.RequisitionItem;
                re.Attachment = await FileUpload.GetFilePathsFromFileAsync(v.Attachment, _environment, "Attachment");
                reList.Add(re);
            }
            return reList;
        }


        private Task<DcProcurement.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}