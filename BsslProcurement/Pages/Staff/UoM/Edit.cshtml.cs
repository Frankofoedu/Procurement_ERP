using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DcProcurement.Contexts;

namespace BsslProcurement.Pages.Staff.UoM
{
    public class EditModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _context;

        public EditModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UnitOfMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitOfMeasurementExists(UnitOfMeasurement.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UnitOfMeasurementExists(int id)
        {
            return _context.UnitOfMeasurements.Any(e => e.Id == id);
        }
    }
}
