using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;

using DcProcurement;
using DcProcurement.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement
{
    public class GroupManagementModel : PageModel
    {
        public string Message { get; set; }
        public string Error { get; set; }


        [BindProperty]
        public GroupViewModel GroupViewModel { get; set; }

        public IList<GroupViewModel>  GroupViewModels { get; set; } = new List<GroupViewModel>();

        private readonly IGroupManagement _groupManagement;

        public GroupManagementModel(IGroupManagement groupManagement)
        {
            _groupManagement = groupManagement;

        }

        public async Task OnGet()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            GroupViewModels = (await _groupManagement.GetAll()).Select(x => new GroupViewModel { Id = x.Id, Name = x.GroupName }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            if (string.IsNullOrWhiteSpace(GroupViewModel.Name))
            {
                Error = "Group must have a name";
                await LoadData();
                return Page();
            }

            try
            {

                var mn = _groupManagement.CreateGroup(GroupViewModel.Name);


                Message = "Group created successfully";

                await LoadData();
                return Page();
            }
            catch (Exception e)
            {
                Error = "An error occurred";

                await LoadData();
                return Page();
            }

            
           
        }
    }
}