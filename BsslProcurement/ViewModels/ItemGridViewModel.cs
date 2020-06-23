using DcProcurement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BsslProcurement.ViewModels
{
   public class ItemGridViewModel
    {
        //used for the grid view
        public int Index { get; set; }
        public RequisitionItem RequisitionItem { get; set; }
        public int? AttachmentId { get; set; }
        public IFormFile Attachment { get; set; }
        public bool isAttachmentChanged { get; set; }
    }
}
