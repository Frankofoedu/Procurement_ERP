using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DcProcurement.Contexts;

namespace BsslProcurement.CategorySetup.Sub
{
    public class DetailsModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _context;

        public DetailsModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        public Busline Busline { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Busline = await _context.Busline.FirstOrDefaultAsync(m => m.Id == id);

            if (Busline == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
