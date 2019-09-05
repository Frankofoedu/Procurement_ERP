using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using DcProcurement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Pages.Staff.Transaction
{

    public class ItemViewModel
    {
        public string ItemCode { get; set; }
        public string Descrip { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string SuppId { get; set; }
        public string SuppName { get; set; }
        public string UnitPrice { get; set; }
        public string Amount { get; set; }
    }

    [Authorize]
    public class NewRequisitionModel : PageModel
    {
        private readonly UserManager<DcProcurement.User> _userManager;
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly DcProcurement.ProcurementDBContext _procContext;



        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public string PrNo { get; set; }

        [BindProperty]
        public List<ItemViewModel> ItemInputModel { get; set; }

        //[BindProperty]
        //public IFormFile File { get; set; }

        [BindProperty]
        public string RequestingDept { get; set; }

        [BindProperty]
        public List<SelectListItem> Departments { get; set; }

        public NewRequisitionModel(UserManager<DcProcurement.User> userManager,
            BSSLSYS_ITF_DEMOContext bsslContext,
            DcProcurement.ProcurementDBContext procContext)
        {
            _userManager = userManager;
            _bsslContext = bsslContext;
            _procContext = procContext;
        }
        public async Task OnGetAsync()
        {

            try
            {
                (PrNo, RequestingDept, Departments) = await GeneratePRNo();
            }
            catch (Exception ex)
            {
                Error = $"An error occured. Please contact Support. {ex.Message}";
            }

        }

        public async Task OnPostAsync(List<IFormFile> files)
        {

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

        static int getValue(int[] arr
                            )
        {
            int n = arr.Length;

            // sum of left elements 
            // array from 0 
            int[] leftSum = new int[n];

            leftSum[0] = arr[0];
            for (int i = 1; i < n; i++)
            {
                leftSum[i] = leftSum[i - 1] + arr[i];
            }

            // Forming right sum  
            // array from n-1 
            int[] rightSum = new int[n];
            rightSum[n - 1] = arr[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                rightSum[i] = rightSum[i + 1] + arr[i];
            }
            // Find the point where left  
            // and right sums are same. 
            for (int i = 1; i < n - 1; i++)
            {
                if (leftSum[i] == rightSum[i])
                {
                    return arr[i];
                }
            }

            return -1;
        }

    }
}