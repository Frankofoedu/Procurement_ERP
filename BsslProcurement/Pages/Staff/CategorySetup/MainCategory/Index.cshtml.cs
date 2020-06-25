using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DcProcurement.Contexts;

namespace BsslProcurement.CategorySetup.Main
{
    public class IndexModel : PageModel
    {
        private readonly BSSLSYS_ITF_DEMOContext _context;

        public IndexModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
