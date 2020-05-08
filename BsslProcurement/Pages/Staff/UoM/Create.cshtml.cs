using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DcProcurement.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.UoM
{
    public class CreateModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _context;

        public CreateModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var check = await _context.UnitOfMeasurements.FirstOrDefaultAsync
                (m => m.Ucode == UnitOfMeasurement.Ucode || m.Uname == UnitOfMeasurement.Uname);
            if (check != null)
            {
                Error = "Error! UoM Code and UoM Name must be unique";
                return Page();
            }

            _context.UnitOfMeasurements.Add(UnitOfMeasurement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
