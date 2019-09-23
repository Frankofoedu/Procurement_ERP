using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    [Authorize]
    public class AllRequisitionModel : PageModel
    {
        private readonly BSSLSYS_ITF_DEMOContext _context;
        public AllRequisitionModel(BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }
        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Procreq2> ApprovedRequests { get; set; }

        public async Task OnGetAsync()
        {
            ApprovedRequests = await _context.Procreq2.ToListAsync();
        }
    }
}