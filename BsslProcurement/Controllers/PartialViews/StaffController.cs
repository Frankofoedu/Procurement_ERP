﻿using System;
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
    [ApiExplorerSettings(IgnoreApi = true)]
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

        public async Task<PartialViewResult> GetStaffWithRank()
        {
            var staffs = await staffLayoutViewModelService.GetAllStaffWithRank();

            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }

        public async Task<PartialViewResult> GetStaffNoRank()
        {
            var staffs = await staffLayoutViewModelService.GetAllStaffNoRank();

            return new PartialViewResult
            {
                ViewName = "Modals/_StaffLayout",
                ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            };
        }
        public async Task<string> GetRankOfStaff(string staffCode )
        {
            var rank = await staffLayoutViewModelService.GetStaffRank(staffCode);

            return rank;
            //return new PartialViewResult
            //{
            //    ViewName = "Modals/_StaffLayout",
            //    ViewData = new ViewDataDictionary<List<StaffLayoutModel>>(ViewData, staffs)
            //};
        }


    }
}