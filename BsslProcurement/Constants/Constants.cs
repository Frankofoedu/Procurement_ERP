using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BsslProcurement.Const
{
    public static class ConstantVals
    {
        public static readonly List<SelectListItem> ProcurementMethod = new List<SelectListItem> {
            new SelectListItem { Text = "DSP – Direct Shopping Procurement", Value = "1" },
            new SelectListItem { Text = "NSB – National Competitive Procurement", Value = "2" },
            new SelectListItem { Text = "ICB – International Competitive Procurement", Value = "3" },
            new SelectListItem { Text = "DSP – Direct Selective Procurement", Value = "4" },
        };

        public const string AttachmentFolder = "Attachments";
    }
}
