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

namespace BsslProcurement.Pages.Staff.ItemRequisition
{
    [NoDiscovery]
    [Authorize]
    [DisplayName("Submitted Requisitions")]
    public class SentRequisitionsModel : PageModel
    {
        private readonly IRequisitionService _requisitionService;

        private readonly UserManager<User> _userManager;
        public SentRequisitionsModel(IRequisitionService requisitionService, UserManager<User> userManager)
        {
            _requisitionService = requisitionService;
            _userManager = userManager;
        }


        public string Message { get; set; }
        public string Error { get; set; }
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

                Requisitions = await _requisitionService.GetSubmittedRequisitionsForLoggedInUser(user.Id);
            }
            catch (Exception ex)
            {

                Error = "An error has occurred. Please contact Admin" + Environment.NewLine + ex.Message;
            }

        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}