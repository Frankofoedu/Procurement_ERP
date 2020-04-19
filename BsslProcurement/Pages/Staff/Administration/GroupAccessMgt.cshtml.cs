using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.AuthModels;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using DcProcurement.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BsslProcurement.Pages.Staff.Administration
{

   

    public class AccessViewModel
    {
        public RazorPagesControllerInfo  RazorPage { get; set; }
        public bool IsSelected { get; set; }
    }
    public class GroupAccessMgtModel : PageModel
    {
        private readonly IRazorPagesControllerDiscovery _razorPagesControllerDiscovery;
        private readonly IGroupManagement _groupManagement;
        private readonly RoleManager<UserRole> _roleManager;
        public GroupAccessMgtModel(IRazorPagesControllerDiscovery razorPagesControllerDiscovery, IGroupManagement groupManagement, RoleManager<UserRole> roleManager)
        {
            _razorPagesControllerDiscovery = razorPagesControllerDiscovery;
            _groupManagement = groupManagement;
            _roleManager = roleManager;
        }


        [BindProperty]
        public string GroupName { get; set; }

        public UserGroup Group { get; set; }
        [BindProperty]
        public List<AccessViewModel> AccessViewModels { get; set; }

        public List<RazorPagesControllerInfo> AccessPages { get; set; } = new List<RazorPagesControllerInfo>();

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }



        public string Message { get; set; }
        public string Error { get; set; }
        public async Task<ActionResult> OnGet()
        {
            await LoadData();

            return Page();
        }

        private async Task LoadData()
        {
            //get group name
            Group = (await _groupManagement.GetById(Id));

            if (Group == null)
            {
                Error = "No group found";
            }

            GroupName = Group.GroupName;

            //check if group access has been setup
            var result = await _roleManager.RoleExistsAsync(GroupName);
            if (result)
            {
                //get access pages for group
                AccessPages = await _groupManagement.GetRolesInGroup(Id);

            }

            AccessViewModels = (await _razorPagesControllerDiscovery.GetControllers()).Select(x => new AccessViewModel { RazorPage = x, IsSelected = AccessPages.Any(j => j.ViewEnginePath == x.ViewEnginePath) }).ToList();
        }

        public async Task<ActionResult> OnPostDeleteAccess()
        {
            try
            {
                await  _groupManagement.ClearGroupRolesAsync(Id);
                Message = "Access cleared";
            }
            catch (Exception e)
            {
                Error = "Error occured. Contact Support :" + e.Message;

            }

            await LoadData();

            return Page();
        }
        public async Task<ActionResult> OnPostDeleteAccessById(string accessId)
        {
            try
            {
                await _groupManagement.RemoveRoleFromGroup(Id,accessId);
                Message = "Access removed";
            }
            catch (Exception e)
            {
                Error = "Error occured. Contact Support :" + e.Message;
            }

            await LoadData();

            return Page();
        }
        public async Task<ActionResult> OnPostSave()
        {
            var selectedPages = AccessViewModels.Where(x => x.IsSelected).Select(x => x.RazorPage).ToList();
            var role = await _roleManager.Roles.FirstOrDefaultAsync(m => m.NormalizedName == GroupName.ToUpper());

            if (role == null)
            {
                var accessJson = JsonConvert.SerializeObject(selectedPages);

                role = new UserRole { Name = GroupName, Access = accessJson };
                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    await LoadData();

                    return Page();
                }
                else
                {
                    try
                    {

                        await _groupManagement.AddUsersInGroupToRole(role.Name, Id);

                        await _groupManagement.AddRoleToGroup(role, Id);

                        Message = "Access saved for group";
                    }
                    catch (Exception ex)
                    {
                        await _roleManager.DeleteAsync(role);
                        Error = "Error occurred while creating access :" + ex.Message;

                    }
                    finally
                    {
                        await LoadData();                   
                    }

                    return Page();

                }
            }
            else
            {
                var access = JsonConvert.DeserializeObject<List<RazorPagesControllerInfo>>(role.Access);
                access.AddRange(selectedPages);
                role.Access = JsonConvert.SerializeObject(access.Distinct());

                await _roleManager.UpdateAsync(role);
                await LoadData();   
                return Page();
            }
                
        }
    }
}


//create role
//assign users to role0