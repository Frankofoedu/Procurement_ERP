using BsslProcurement.AuthModels;
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
    public class DynamicAuthorizationOptions
    {
        /// <summary>
        /// Sets the default admin user. Authorization check will be suppressed.
        /// </summary>
        /// <value>The default admin user.</value>
        public string DefaultAdminUser { get; set; }
    }

    [HtmlTargetElement("secure-content")]
    public class SecureContentTagHelper : TagHelper
    {
        private readonly ProcurementDBContext _dbContext;
        private readonly DynamicAuthorizationOptions _authorizationOptions;

        public SecureContentTagHelper(ProcurementDBContext dbContext, DynamicAuthorizationOptions authorizationOptions)
        {
            _dbContext = dbContext;
            _authorizationOptions = authorizationOptions;
        }

        [HtmlAttributeName("asp-viewenginepath")]
        public string Path { get; set; }



        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            var user = ViewContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            if (user.Identity.Name.Equals(_authorizationOptions.DefaultAdminUser, StringComparison.CurrentCultureIgnoreCase))
                return;

            var roles = await (
                from usr in _dbContext.Users
                join userRole in _dbContext.UserRoles on usr.Id equals userRole.UserId
                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                where usr.UserName == user.Identity.Name
                select role
            ).ToListAsync();

            

            foreach (var role in roles)
            {
                if (role.Access == null)
                    continue;

                var accessList = JsonConvert.DeserializeObject<IEnumerable<RazorPagesControllerInfo>>(role.Access);
                if (accessList.Any(a => a.ViewEnginePath == Path))
                    return;
            }

            output.SuppressOutput();
        }
    }
}