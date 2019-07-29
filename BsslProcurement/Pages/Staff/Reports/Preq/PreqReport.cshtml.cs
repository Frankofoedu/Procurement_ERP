using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.Reports.Preq
{
    public class PreqReportModel : PageModel
    {
        readonly ProcurementDBContext _context;
        public PreqReportModel(ProcurementDBContext context)
        {
            _context = context;
        }
        public IList<CompanyInfo> ApprovedCompanyInfos { get; set; }
        public IList<CompanyInfo> RejectedCompanyInfos { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }


        public void OnGet()
        {
            ApprovedCompanyInfos = _context.CompanyInfo.Where(comp => comp.Approved).ToList();

            RejectedCompanyInfos = _context.CompanyInfo.Where(comp => comp.Disqualified).ToList();

           
        }

    }
}
