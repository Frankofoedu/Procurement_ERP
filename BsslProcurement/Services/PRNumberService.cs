using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class PRNumberService : IPRNumberService
    {
        private readonly ProcurementDBContext _procurementDBContext;
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;

        public PRNumberService(ProcurementDBContext procurementDBContext, BSSLSYS_ITF_DEMOContext bsslContext)
        {
            _procurementDBContext = procurementDBContext;
            _bsslContext = bsslContext;
        }

        public async Task<PRNumberServiceDataViewModel> GetDashedPRNumberAndDepartmentDataAsync(string userCode)
        {
            //get current user details from staff table. From the fucking staff table
            var staff = await _bsslContext.Useracct.FirstOrDefaultAsync(x => x.Userid == userCode);

            if (staff == null) throw new Exception("Staff Not Setup");
            //get company prefix 
            var companyCode = staff.Compcode;
            var comp = await _bsslContext.Compdata.FirstOrDefaultAsync(m => m.Compcode == companyCode);

            if (comp == null) throw new Exception("No Company Prefix found");

            var compPrefix = comp.Names;

            var deptCode = (await _bsslContext.Stafftab.FirstOrDefaultAsync(st => st.Staffid == staff.Userid)).Deptcode;

            var Depts = await _bsslContext.Codestab.Where(opt => opt.Option1 == "F5").ToListAsync();

            var Dept = Depts.FirstOrDefault(cd => cd.Code.Trim() == deptCode.Trim());

            var DeptPrefix = Dept.Prefixcode;

            var year = DateTime.Now.Year.ToString();

            var data = new PRNumberServiceDataViewModel()
            {
                departments = Depts.Select(x => x.Desc1).ToList(),
                prnumber = $"{compPrefix.Trim()}/{deptCode.Trim()}/{DeptPrefix.Trim()}/{year}/-----",
                requestDept = Dept.Desc1,
                requestDeptCode = deptCode
            };

            return data;
        }

        public async Task<string> GetNewPRNumberAsync(string DashedPRno)
        {
            var DPRNarr = DashedPRno.Split('/');

            //get last req no
            var lastReqNo = await _procurementDBContext.PRNos.OrderByDescending(x => x.Id).
                FirstOrDefaultAsync(x => x.CompCode == DPRNarr[0].Trim() && x.DeptCode == DPRNarr[1].Trim() && x.DeptPrefix == DPRNarr[2].Trim() && x.Year == DPRNarr[3]);
            var nextSerialNo = "";

            //Generate next requisition number : Pattern = itf/deptcode/deptprefix/year/serial no
            if (lastReqNo != null)
            { 
                nextSerialNo = (Convert.ToInt32(lastReqNo.SerialNo, CultureInfo.InvariantCulture) + 1).ToString("00000", CultureInfo.InvariantCulture); 
            }
            else { 
                nextSerialNo = "00001";
            }


            _procurementDBContext.Add(new PRNo()
            {
                CompCode = DPRNarr[0].Trim(),
                DeptCode = DPRNarr[1].Trim(),
                DeptPrefix = DPRNarr[2].Trim(),
                SerialNo = nextSerialNo,
                Year = DPRNarr[3]
            });

            await _procurementDBContext.SaveChangesAsync();

            return ($"{DPRNarr[0].Trim()}/{DPRNarr[1].Trim()}/{DPRNarr[2].Trim()}/{DPRNarr[3]}/{nextSerialNo}");
        }
    }
}
