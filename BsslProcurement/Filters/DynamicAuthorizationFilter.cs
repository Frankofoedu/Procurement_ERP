using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using BsslProcurement.AuthModels;
using Microsoft.EntityFrameworkCore;
using BsslProcurement.TagHelpers;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using BsslProcurement.Filters.Attributes;

namespace BsslProcurement.Filters
{
    public class DynamicAuthorizationOptions
    {
        /// <summary>
        /// Sets the default admin user. Authorization check will be suppressed.
        /// </summary>
        /// <value>The default admin user.</value>
        public string DefaultAdminUser { get; set; }

    }
    public class DynamicAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ProcurementDBContext _dbContext; 
        private readonly DynamicAuthorizationOptions _authorizationOptions;
        private readonly PageLoader _pageLoader;

        public DynamicAuthorizationFilter(PageLoader pageLoader,ProcurementDBContext dbContext,DynamicAuthorizationOptions authorizationOptions)
        {
            _dbContext = dbContext;
            _authorizationOptions = authorizationOptions;
            _pageLoader = pageLoader;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //checks if it is a controller 
            if (context.ActionDescriptor.GetType() == typeof(ControllerActionDescriptor))
            {
                return;
            }
            //checks if page allowsanonymous
            if (!IsProtectedPage(context))
                return;

            if (!IsUserAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var viewPath = GetViewPath(context);
            var userName = context.HttpContext.User.Identity.Name;
           

            //checks if user is admin
            if (userName.Equals(_authorizationOptions.DefaultAdminUser, StringComparison.CurrentCultureIgnoreCase))
                return;

            //checks if page can be accessed by all staff
            var page = (PageActionDescriptor)context.ActionDescriptor;
           var compiledPage = await _pageLoader.LoadAsync(page);
            if (compiledPage.EndpointMetadata.OfType<NoDiscoveryAttribute>().Any())
            {
                return;
            }

            var roles = await (
                from user in _dbContext.Staffs
                join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                where user.UserName == userName
                select role
            ).ToListAsync();



            foreach (var role in roles)
            {
                var accessList = JsonConvert.DeserializeObject<IEnumerable<RazorPagesControllerInfo>>(role.Access);
                if (accessList.Any(a => a.ViewEnginePath == viewPath))
                    return;
            }

            context.Result = new ForbidResult();

        }

        private bool IsUserAuthenticated(AuthorizationFilterContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }

        private bool IsProtectedPage(AuthorizationFilterContext context)
        {

            var pageDescriptor = (PageActionDescriptor)context.ActionDescriptor;

            var allowAnonymousAttribute = pageDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().FirstOrDefault();

            if (allowAnonymousAttribute != null)
                return false;

           // var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
           // var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = pageDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>().FirstOrDefault();
            if (authorizeAttribute != null)
                return true;

            //authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            //if (authorizeAttribute != null)
            //    return true;

            return false;
        }
        private string GetViewPath(AuthorizationFilterContext context)
        {
            var pageDescriptor = (PageActionDescriptor)context.ActionDescriptor;
            var path = pageDescriptor.ViewEnginePath;

            return path;
        }
    }
}
