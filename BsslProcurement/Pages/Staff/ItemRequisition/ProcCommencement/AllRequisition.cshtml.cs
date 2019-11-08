using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcCommencement
{
    [Authorize]
    public class AllRequisitionModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        public AllRequisitionModel(ProcurementDBContext context)
        {
            _context = context;
        }
        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Requisition> Requisitions { get; set; }

        public async Task OnGetAsync()
        {
            Requisitions = await _context.Requisitions.Include(x => x.RequisitionItems).ToListAsync();
        }
    }
}