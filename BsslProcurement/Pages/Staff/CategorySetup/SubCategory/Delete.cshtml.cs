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
    public class DeleteModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _context;

        public DeleteModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Busline = await _context.Busline.FindAsync(id);

            if (Busline != null)
            {
                _context.Busline.Remove(Busline);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
