using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DcProcurement.Contexts;

namespace BsslProcurement.Pages.Staff.UoM
{
    public class DeleteModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _context;

        public DeleteModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UnitOfMeasurement = await _context.UnitOfMeasurements.FirstOrDefaultAsync(m => m.Id == id);

            if (UnitOfMeasurement == null)
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

            UnitOfMeasurement = await _context.UnitOfMeasurements.FindAsync(id);

            if (UnitOfMeasurement != null)
            {
                _context.UnitOfMeasurements.Remove(UnitOfMeasurement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
