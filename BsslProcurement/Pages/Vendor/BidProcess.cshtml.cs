using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Contexts;
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

        private readonly ProcurementDBContext _context;

        public BidProcessModel(ProcurementDBContext context)
        {
            _context = context;

        }

        public void OnGet()
        {
            InviteInput = new ViewModels.InvitePartialInputViewModel();
            DeclarationInput = new ViewModels.DeclarationPartialInputViewModel();
            GeneralInput = new ViewModels.GeneralInformationPartialViewModel();


        }

        public void OnPost()
        {
            var m = DeclarationInput.Name;
        }
    }
}