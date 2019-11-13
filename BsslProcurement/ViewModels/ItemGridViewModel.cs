using DcProcurement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BsslProcurement.ViewModels
{
   public class ItemGridViewModel
    {

        public RequisitionItem RequisitionItem { get; set; }
        public IFormFile Attachment { get; set; }
    }
}
