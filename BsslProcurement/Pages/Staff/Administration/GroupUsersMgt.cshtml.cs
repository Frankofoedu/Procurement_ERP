using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff.Administration
{
    [Authorize]
    public class GroupUsersMgtModel : PageModel
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public IList<GroupUsersViewModel> GroupUsersViewModel { get; set; }


        [BindProperty]
        public StaffWithEmailViewModel StaffEmailListObj { get; set; } = new StaffWithEmailViewModel();
        [BindProperty]
        public string GroupName { get; set; }
        private readonly IGroupManagement _groupManagement;
        private readonly DcProcurement.ProcurementDBContext _context;

        public GroupUsersMgtModel(IGroupManagement groupManagement, DcProcurement.ProcurementDBContext context)
        {
            _groupManagement = groupManagement;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            try
            {

                await LoadData(Id);
            }
            catch (KeyNotFoundException)
            {
                Error = "Error: No Group found";
            }
            return Page();
        }

        private async Task LoadData(int id)
        {
            var grp = await _context.UserGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (grp != null)
            {
                GroupName = grp.GroupName;
            }
            GroupUsersViewModel = await _groupManagement.GetAllUsersInGroup(id);

            var idsToBeRemoved = new HashSet<string>(GroupUsersViewModel.Select(item => item.Staff.Id));

            StaffEmailListObj.StaffWithEmailList = StaffEmailListObj.GetStaffWithEmailList(_context.Staffs.ToList());

            StaffEmailListObj.StaffWithEmailList.RemoveAll(x => idsToBeRemoved.Contains(x.Id));

        }

        public async Task<IActionResult> OnPost()
        {
            var selectedStaff = StaffEmailListObj.StaffWithEmailList.Where(m => m.isSelected).Select(x => x.Id).ToList();
            try
            {
                await _groupManagement.AddListUserToGroup(selectedStaff, Id);
                Message = "Staffs added to group";
            }
            catch (KeyNotFoundException)
            {
                Error = "Error: No Group found";
            }
            finally
            {
                await LoadData(Id);
            }
            //return Page();
            return RedirectToPage(new {id = Id });
        }

        public async Task<IActionResult> OnPostDeleteStaff(string staffId)
        {
            try
            {
               await _groupManagement.RemoveUserFromGroupAsync(staffId, Id);
                Message = "Staff removed from group";
            }
            catch (Exception e)
            {
                Error = "Error: Please contact support" + Environment.NewLine + e.Message;
            }
            finally
            {
                await LoadData(Id);
            }

            return Page();
        }
    }
}