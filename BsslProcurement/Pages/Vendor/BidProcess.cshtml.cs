using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement
{

    [Authorize(AuthenticationSchemes = "Vendors")]
    public class BidProcessModel : PageModel
    {
        [BindProperty]
        public ViewModels.InvitePartialInputViewModel InviteInput { get; set; }

        [BindProperty]
        public ViewModels.DeclarationPartialInputViewModel DeclarationInput { get; set; }

        [BindProperty]
        public ViewModels.GeneralInformationPartialViewModel GeneralInput { get; set; }

        public void OnGet()
        {

        }
    }
}