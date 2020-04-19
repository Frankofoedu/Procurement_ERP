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
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement
{
    public class GroupManagementModel : PageModel
    {
        public string Message { get; set; }
        public string Error { get; set; }


        [BindProperty]
        public GroupViewModel GroupViewModel { get; set; } = new GroupViewModel();

        public IList<GroupViewModel> GroupViewModels { get; set; } = new List<GroupViewModel>();

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
            GroupViewModel.Name = "";
            ModelState.Clear();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                Error = " Please select group";
                await LoadData();
                return Page();

            }

            try
            {
                await _groupManagement.DeleteGroup(id.Value);


                Message = "Group deleted";
            }
            catch (Exception e)
            {
                Error = "An error occured";
            }
            finally
            {
                await LoadData();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
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
               await _groupManagement.CreateGroup(GroupViewModel.Name);
                Message = "Group created successfully";
            }
            catch (DbUpdateException)
            {
                Error = "Group name already exists";
            }
            catch (Exception e)
            {
                Error = "An error occurred";
            }
            finally
            {
                await LoadData();
            }


            return Page();
        }
    }
}