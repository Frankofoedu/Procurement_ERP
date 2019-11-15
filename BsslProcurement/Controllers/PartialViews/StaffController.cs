using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Controllers.PartialViews
{
    [Route("[controller]/[action]")]
    public class StaffController : Controller
    {
        private readonly IStaffLayoutViewModelService staffLayoutViewModelService;
        public StaffController(IStaffLayoutViewModelService _staffLayoutViewModelService)
        {
            staffLayoutViewModelService = _staffLayoutViewModelService;
        }
        public async Task<PartialViewResult> GetStaffWorkflowPartial(int id)
        {
            var staffs = await staffLayoutViewModelService.GetAllStaffInWorkFlow(id);

            //var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }
    }
}