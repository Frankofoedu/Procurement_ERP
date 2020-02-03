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

namespace BsslProcurement.Pages.Staff.ItemRequisition.ProcApproval
{
    [Authorize]
    public class AllRequisitionModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IRequisitionService requisitionService;

        private readonly UserManager<User> _userManager;
        public AllRequisitionModel(ProcurementDBContext context, IRequisitionService _requisitionService, UserManager<User> userManager)
        {
            _context = context;
            requisitionService = _requisitionService;
            _userManager = userManager;
        }
        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Requisition> Requisitions { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                Requisitions = await requisitionService.GetRequisitionsForLoggedInUser(user.Id);
            }
            catch (Exception ex)
            {

                Error = "Error has occured. Please contact Admin." + Environment.NewLine + ex.Message;
            }
           
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}