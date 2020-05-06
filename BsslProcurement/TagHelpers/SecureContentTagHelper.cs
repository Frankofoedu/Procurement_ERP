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
   

    [HtmlTargetElement("secure-content")]
    public class SecureContentTagHelper : Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper
    {
        private readonly ProcurementDBContext _dbContext;
        private readonly DynamicAuthorizationOptions _authorizationOptions;


        public SecureContentTagHelper(ProcurementDBContext dbContext, DynamicAuthorizationOptions authorizationOptions, IHtmlGenerator generator) : base(generator)
        {
            _dbContext = dbContext;
            _authorizationOptions = authorizationOptions;
        }


        /// <summary>
        /// Set to true if all staff can view
        /// </summary>

        [HtmlAttributeName("proc-nodiscovery")]
        public bool NoDiscovery { get; set; }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "a";
            var user = ViewContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            if (user.Identity.Name.Equals(_authorizationOptions.DefaultAdminUser, StringComparison.CurrentCultureIgnoreCase))
                return;

            //check if page can be accessed by all staff
            if (NoDiscovery)
            {
                return;
            }

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
                if (accessList.Any(a => a.ViewEnginePath == Page))
                    return;
            }

            output.SuppressOutput();
        }
    }
}