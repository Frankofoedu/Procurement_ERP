using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Filters.Attributes;
using BsslProcurement.Interfaces;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement
{
    [DisplayName("Approved Requisitions")]
    [Authorize]
    [NoDiscovery]
    public class ApprovedRequisitionsModel : PageModel
    {
        private readonly ProcurementDBContext _context;
        private readonly IRequisitionService requisitionService;
        private readonly UserManager<User> _userManager;



        public ApprovedRequisitionsModel(ProcurementDBContext context, IRequisitionService _requisitionService, UserManager<User> userManager)
        {
            _context = context;
            requisitionService = _requisitionService;
            _userManager = userManager;
        }



        public string Message { get; set; }
        public string Error { get; set; }

        [BindProperty]
        public List<Requisition> Requisitions { get; set; }
        public async Task OnGetAsync(string message)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    Message = message;
                }
                var user = await GetCurrentUserAsync();

                Requisitions = await requisitionService.GetApprovedRequisitionsForLoggedInUser(user.Id);
            }
            catch (Exception ex)
            {

                Error = "An error has occurred. Please contact Admin" + Environment.NewLine + ex.Message;
            }


        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}