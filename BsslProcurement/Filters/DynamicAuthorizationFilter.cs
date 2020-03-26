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

namespace BsslProcurement.Filters
{
    public class DynamicAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ProcurementDBContext _dbContext;

        public DynamicAuthorizationFilter(ProcurementDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!IsProtectedPage(context))
                return;

            if (!IsUserAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
           var viewPath = GetViewPath(context);
            var userName = context.HttpContext.User.Identity.Name;

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
                if (accessList.Any(a => a.AreaName == viewPath))
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
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return false;

            var pageDescriptor = (PageActionDescriptor)context.ActionDescriptor;
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
