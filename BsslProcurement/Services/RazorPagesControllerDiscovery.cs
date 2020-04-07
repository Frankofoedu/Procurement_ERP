using BsslProcurement.AuthModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace BsslProcurement.Services
{
    public interface IRazorPagesControllerDiscovery
    {
        Task<IEnumerable<RazorPagesControllerInfo>> GetControllers();
    }
    public class RazorPagesControllerDiscovery : IRazorPagesControllerDiscovery
    {
        private List<RazorPagesControllerInfo> _razorPages;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly PageLoader _pageLoader;
        public RazorPagesControllerDiscovery(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, PageLoader pageLoader)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;

            _pageLoader = pageLoader;
        }
        public async Task<IEnumerable<RazorPagesControllerInfo>> GetControllers()
        {


            var t = _actionDescriptorCollectionProvider
                  .ActionDescriptors.Items
                  .OfType<PageActionDescriptor>().Where(x => x.AreaName != "Identity").Select(async x => await _pageLoader.LoadAsync(x));

            var m = await Task.WhenAll(t);
              _razorPages = m.Select(x => new RazorPagesControllerInfo { DisplayName = x.EndpointMetadata.OfType<System.ComponentModel.DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? x.DisplayName, ViewEnginePath = x.ViewEnginePath   })
                .ToList();

            //foreach (var actionDescriptors in items)
            //{
            //    if (!actionDescriptors.Any())
            //        continue;

            //    var actionDescriptor = actionDescriptors.First();
            //    var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
            //    var currentController = new MvcControllerInfo
            //    {
            //        AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
            //        DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
            //        Name = actionDescriptor.ControllerName,
            //    };

            //    var actions = new List<MvcActionInfo>();
            //    foreach (var descriptor in actionDescriptors.GroupBy(a => a.ActionName).Select(g => g.First()))
            //    {
            //        var methodInfo = descriptor.MethodInfo;
            //        actions.Add(new MvcActionInfo
            //        {
            //            ControllerId = currentController.Id,
            //            Name = descriptor.ActionName,
            //            DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
            //        });
            //    }

            //    currentController.Actions = actions;
            //    _razorPages.Add(currentController);
            //}

            return _razorPages;
        }



    }

}
