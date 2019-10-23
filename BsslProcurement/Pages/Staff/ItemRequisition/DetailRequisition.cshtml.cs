using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    public class DetailRequisitionModel : PageModel
    {

        private readonly ProcurementDBContext _context;
        public DetailRequisitionModel(ProcurementDBContext context)
        {
            _context = context;
        }
        public string Message { get; set; }
        public string Error { get; set; }
        [BindProperty]
        public Requisition Requisition { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Requisition = await _context.Requisitions.Include(re => re.RequisitionItems).Include(re => re.Attachments).FirstOrDefaultAsync(x=> x.Id == id); 

            if (Requisition == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}