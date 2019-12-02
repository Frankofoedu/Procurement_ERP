using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.ItemRequisition.BidPreparation
{
    [Authorize]
    public class ERFXModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        public ERFXModel(ProcurementDBContext context)
        {
            _context = context;
        }

        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {

        }
    }
}