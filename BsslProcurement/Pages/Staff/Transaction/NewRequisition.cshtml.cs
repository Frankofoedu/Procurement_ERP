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

namespace BsslProcurement.Pages.Staff.Transaction
{


    [Authorize]
    public class NewRequisitionModel : PageModel
    {
        private readonly UserManager<DcProcurement.User> _userManager;
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly DcProcurement.ProcurementDBContext _procContext;
        private IHostingEnvironment _environment;



        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public string PrNo { get; set; }

        [BindProperty]
        public List<RequisitionItem> RequisitionItems { get; set; }

        //[BindProperty]
        //public IFormFile File { get; set; }

        [BindProperty]
        public string RequestingDept { get; set; }

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

        public async Task OnPostAsync(List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var filepaths = (await FileUpload.GetFilePathsAsync(files, _environment));

                    Requisition.Attachments = filepaths;

                    _procContext.Requisitions.Add(Requisition);

                    _procContext.SaveChanges();

                    Message = "Requisition Added successfully";
                }
                catch (Exception ex)
                {
                    Error = "An error has occurred." + Environment.NewLine + ex.Message;
                }

            }
            await LoadData();
        }

        private async Task LoadData()
        {
            (PrNo, RequestingDept, Departments) = await GeneratePRNo();
        }
        public PartialViewResult OnGetStaffPartial()
        {

            //get all staff and thier ranks
            var staffs = _bsslContext.Stafftab.Select(x => new StaffLayoutModel { Staff = x, Rank = _bsslContext.Codestab.Where(m => m.Option1 == "f4" && m.Code == x.Positionid).FirstOrDefault().Desc1 }).ToList();


            //           var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "_StaffLayout",
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

        private async Task<(string, string, List<SelectListItem>)> GeneratePRNo()
        {
            //get current logged in user
            var u = await GetCurrentUserAsync();

            //get staff object
            var user = _procContext.Staffs.Find(u.Id);


            //get current user details from staff table. From the fucking staff table
            var staff = _bsslContext.Useracct.FirstOrDefault(x => x.Userid == user.StaffCode);

            if (staff != null)
            {
                //get company prefix 
                var companyCode = staff.Compcode;
                var comp = _bsslContext.Compdata.FirstOrDefault(m => m.Compcode == companyCode);

                if (comp != null)
                {
                    var compPrefix = comp.Names;

                    var deptCode = _bsslContext.Stafftab.FirstOrDefault(st => st.Staffid == staff.Userid).Deptcode;



                    var Depts = _bsslContext.Codestab.Where(opt => opt.Option1 == "F5");

                    var Dept = Depts.FirstOrDefault(cd => cd.Code == deptCode);

                    var DeptPrefix = Dept.Prefixcode;

                    var year = DateTime.Now.Year.ToString();

                    // implement serial no
                    var PrNo = _procContext.PRNos.OrderByDescending(t => t.LastUsedSerialNo).FirstOrDefault();

                    string serialNo = "";
                    if (PrNo == null)
                    {
                        serialNo = "00001";
                    }
                    else
                    {
                        serialNo = (Convert.ToInt32(PrNo.LastUsedSerialNo) + 1).ToString("0000");
                    }

                    //itf/deptcode/deptprefix/year/serial no

                    return ($"{compPrefix.Trim()}/{deptCode.Trim()}/{DeptPrefix.Trim()}/{year}/{serialNo}", Dept.Desc1, Depts.Select(depts => new SelectListItem { Text = depts.Desc1 }).ToList());

                }

                throw new Exception("No Company Prefix found");
            }


            throw new Exception("STaff Not Setup");
        }

        private Task<DcProcurement.User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        
    }
}