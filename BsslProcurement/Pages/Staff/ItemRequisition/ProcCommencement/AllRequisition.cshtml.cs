using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using DcProcurement;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcCommencement
{
    [Authorize]
    public class AllRequisitionModel : PageModel
    {
        private readonly IProcurementService _procurementService;

        private readonly UserManager<User> _userManager;
        public AllRequisitionModel(IProcurementService procurementService, UserManager<User> userManager)
        {
            _procurementService = procurementService;
            _userManager = userManager;
        }
        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Requisition> Requisitions { get; set; }

        public async Task OnGetAsync()
        {

            Requisitions = await _procurementService.GetApprovedRequisitions();
        }

        //private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}