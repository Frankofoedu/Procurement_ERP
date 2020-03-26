using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.AuthModels;
using BsslProcurement.Interfaces;
using BsslProcurement.Services;
using DcProcurement.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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


        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [BindProperty]
        public string GroupName { get; set; }

        public UserGroup Group { get; set; }
        [BindProperty]
        public List<AccessViewModel> AccessViewModels { get; set; }

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

            AccessViewModels = (await _razorPagesControllerDiscovery.GetControllers()).Select(x => new AccessViewModel { RazorPage = x }).ToList();
        }

        public async Task<ActionResult> OnPost()
        {
           // RazorPagesControllers = await _razorPagesControllerDiscovery.GetControllers();
           var selectedPages = AccessViewModels.Where(x => x.IsSelected).Select(x => x.RazorPage).ToList();


            var accessJson = JsonConvert.SerializeObject(selectedPages);
            var role = new UserRole { Name = GroupName, Access = accessJson };
            

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
               await _groupManagement.AddUsersInGroupToRole(role.Name, Id);


                Message = "Access saved for group";
                await LoadData();
                return Page();
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return Page();
        }
    }
}


//create role
//assign users to role0