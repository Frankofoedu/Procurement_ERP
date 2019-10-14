using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    public class DetailRequisitionModel : PageModel
    {

        private readonly ProcurementDBContext _context;
        public DetailRequisitionModel(ProcurementDBContext context)
        {
            _context = context;
        }
        public string Message { get; set; }
        public string Error { get; set; }
        public Requisition Requisition { get; set; }
        public void OnGet(int id)
        {
            Requisition = _context.Requisitions.FirstOrDefault(x => x.Id == id);

        }
    }
}