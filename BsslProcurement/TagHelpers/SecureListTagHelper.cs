using BsslProcurement.AuthModels;
using BsslProcurement.Filters;
using DcProcurement;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.TagHelpers
{
    [HtmlTargetElement("li", Attributes = "is-admin-control")]
    public class SecureListTagHelper : TagHelper
    {


        private readonly DynamicAuthorizationOptions _authorizationOptions;

        public SecureListTagHelper(DynamicAuthorizationOptions authorizationOptions) 
        {

            _authorizationOptions = authorizationOptions;
        }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            var user = ViewContext.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            if (user.Identity.Name.Equals(_authorizationOptions.DefaultAdminUser, StringComparison.CurrentCultureIgnoreCase))
                return;

            output.SuppressOutput();
        }

    }
}
