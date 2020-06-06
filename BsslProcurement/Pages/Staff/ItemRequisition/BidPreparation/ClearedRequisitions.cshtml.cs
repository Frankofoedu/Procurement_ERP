using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff.ItemRequisition.BidPreparation
{
    [Authorize]
    public class ClearedRequisitionsModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IProcurementService _service;

        public ClearedRequisitionsModel(ProcurementDBContext context, IProcurementService service)
        {
            _context = context;
            _service = service;
        }

        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Requisition> Requisitions { get; set; }

        public async Task OnGet()
        {
            Requisitions = await _service.GetBudgetClearedRequisitions();
            if (Requisitions == null)
            {
                Requisitions = new List<Requisition>();
            }
        }
    }
}