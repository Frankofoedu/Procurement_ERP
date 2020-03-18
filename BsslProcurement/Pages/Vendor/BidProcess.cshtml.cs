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
        public void OnGet()
        {

        }
    }
}