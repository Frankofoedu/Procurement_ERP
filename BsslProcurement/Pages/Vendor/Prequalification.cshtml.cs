﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Vendor
{
    public class PrequalificationModel : PageModel
    {
        public string Message { get; set; }
        public string Error { get; set; }

        public void OnGet()
        {

        }
    }
}