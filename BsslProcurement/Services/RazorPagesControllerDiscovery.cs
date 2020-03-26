using BsslProcurement.AuthModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public interface IRazorPagesControllerDiscovery
    {
        IEnumerable<RazorPagesControllerInfo> GetControllers();
    }
    public class RazorPagesControllerDiscovery : IRazorPagesControllerDiscovery
    {
        private List<RazorPagesControllerInfo> _razorPages;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RazorPagesControllerDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        public IEnumerable<RazorPagesControllerInfo> GetControllers()
        {
            if (_razorPages != null)
                return _razorPages;

            _razorPages = new List<RazorPagesControllerInfo>();

            var items = _actionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .Where(descriptor => descriptor.GetType() == typeof(PageActionDescriptor))
                .Select(descriptor => (PageActionDescriptor)descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;

                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
                var currentController = new MvcControllerInfo
                {
                    AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName,
                };

                var actions = new List<MvcActionInfo>();
                foreach (var descriptor in actionDescriptors.GroupBy(a => a.ActionName).Select(g => g.First()))
                {
                    var methodInfo = descriptor.MethodInfo;
                    actions.Add(new MvcActionInfo
                    {
                        ControllerId = currentController.Id,
                        Name = descriptor.ActionName,
                        DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    });
                }

                currentController.Actions = actions;
                _razorPages.Add(currentController);
            }

            return _razorPages;
        }
    }

}
