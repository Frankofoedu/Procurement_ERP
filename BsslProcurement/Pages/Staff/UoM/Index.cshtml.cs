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
    public class IndexModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _context;

        public IndexModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        public IList<UnitOfMeasurement> UnitOfMeasurement { get;set; }

        public async Task OnGetAsync()
        {
            UnitOfMeasurement = await _context.UnitOfMeasurements.ToListAsync();
        }
    }
}
