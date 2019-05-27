using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff
{
    public class PrequalificationCriteriaModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public PrequalificationCriteriaModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProcurementCriteria ProcurementCriteria { get; set; }
        [BindProperty]
        public int itemId { get; set; }
        public List<ProcurementCriteria> ProcurementCriterias { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public IActionResult OnGet(int? id, int? delId)
        {
            if (id == null)
            { return LocalRedirect("~/Staff/Items"); }
            itemId = id.Value;

            if (delId != null)
            {
                var delItem = _context.ProcurementCriteria.FirstOrDefault(x => x.Id == delId);

                if (delItem !=null)
                {
                    _context.ProcurementCriteria.Remove(delItem);
                    _context.SaveChanges();
                    Message = "Deleted Successfully";
                }
            }

            ProcurementCriterias = _context.ProcurementCriteria.Where(m => m.ProcurementItemId == itemId).AsNoTracking().ToList();
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return Page();
            }

            var check = _context.ProcurementCriteria.FirstOrDefault(x => x.ProcurementItemId ==
                itemId && x.CriteriaDescription == ProcurementCriteria.CriteriaDescription);

            if (check != null)
            {
                Error = "This Criteria already exist for this Item.";
                return Page();
            }

            ProcurementCriteria.ProcurementItemId = itemId;
            _context.ProcurementCriteria.Add(ProcurementCriteria);
            _context.SaveChanges();

            ProcurementCriterias = _context.ProcurementCriteria.Where(m => m.ProcurementItemId == itemId).AsNoTracking().ToList();

            Message = "Added Successfully";
            return Page();
        }

        
    }
}