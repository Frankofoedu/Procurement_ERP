using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.DynamicData
{
    public class SubCategoriesModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        public SubCategoriesModel(ProcurementDBContext context)
        {
            _context = context;
        }

        public JsonResult OnGet(int id)
        {
            var listCat = _context.ProcurementSubcategories.AsNoTracking().Where(m => m.ProcurementCategoryId == id).Select(x => new { value = x.Id, name = x.Name }).ToList();

            return listCat.Any() ? new JsonResult(listCat) : null;
        }

    }
}