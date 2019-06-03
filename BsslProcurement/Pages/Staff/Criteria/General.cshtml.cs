using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.Criteria
{
    public class GeneralModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public GeneralModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DcProcurement.Criteria GCriteria { get; set; }

        public string Message { get; set; }
        public string Error { get; set; }

        public IActionResult OnGet(int? delId)
        {

            //if (delId != null)
            //{
            //    var delItem = _context.ProcurementCriteria.FirstOrDefault(x => x.Id == delId);

            //    if (delItem != null)
            //    {
            //        _context.ProcurementCriteria.Remove(delItem);
            //        _context.SaveChanges();
            //        Message = "Deleted Successfully";
            //    }
            //}

            return Page();
        }


        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An Error occured. Please check the data and try again.";
                return;
            }

            var check = _context.CategoryCriterias.FirstOrDefault(x => x.CriteriaDescription == GCriteria.CriteriaDescription);

            if (check != null)
            {
                Error = "Criteria with this Description already exist.";
                return;
            }

            _context.Criterias.Add(GCriteria);
            _context.SaveChanges();

            Message = "Added Successfully";
            return;
        }

    }
}