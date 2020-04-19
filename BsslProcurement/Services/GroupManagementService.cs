using BsslProcurement.AuthModels;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class GroupManagementService : IGroupManagement
    {
        private readonly ProcurementDBContext procurementdbcontext;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public GroupManagementService(ProcurementDBContext _procurementdbcontext, RoleManager<UserRole> roleManager, UserManager<User> userManager)
        {
            procurementdbcontext = _procurementdbcontext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task AddListUserToGroup(List<string> userIds, int groupId)
        {
            if (userIds != null && userIds.Count> 0)
            {
                //get staff(s) from staff table 
                var staffs = procurementdbcontext.Staffs.Where(x => userIds.Contains(x.Id)).ToList();


                  var staffVm = staffs.Select(x => new StaffUserGroup { StaffId = x.Id, UserGroupId = groupId }).ToList();

                //get selected group
                var grp = await procurementdbcontext.UserGroups.Include(x => x.UserRole).Include(x => x.Staffs).FirstOrDefaultAsync( x => x.Id == groupId);

                if (grp != null)
                {
                    var oldstaffIds = grp.Staffs.Select(x => x.StaffId);
                    staffVm.RemoveAll(st => oldstaffIds.Contains(st.StaffId));

                    grp.Staffs.AddRange(staffVm);

                    //check if group access was previously created
                    if (grp.UserRole != null)
                    {
                        foreach (var staff in staffs)
                        {

                            await _userManager.AddToRoleAsync(staff,grp.GroupName);
                        }
                    }

                 await   procurementdbcontext.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Group Not Found");
                }

            }
        }

        public async Task AddUsersInGroupToRole(string roleName, int groupId)
        {

            //get group
            var grp = procurementdbcontext.UserGroups.Include(x => x.Staffs).ThenInclude(x => x.Staff).FirstOrDefault(x => x.Id == groupId);

            //get staff in group
            var staffsGrps = grp?.Staffs;

            if (staffsGrps.Any() && staffsGrps.Count> 0)
            {
                foreach (var staffgrp in staffsGrps)
                {
                    var staff = staffgrp.Staff;
                    await _userManager.AddToRoleAsync(staff, roleName);
                }
            }

        }
        public async Task AddRoleToGroup(UserRole role, int groupId)
        {

            //get group
            var grp = procurementdbcontext.UserGroups.Include(x => x.Staffs).FirstOrDefault(x => x.Id == groupId);

            if (grp != null && role != null)
            {
                grp.AddRoleToGroup(role.Id);

               await procurementdbcontext.SaveChangesAsync();
            }
        }

        public void AddUserToGroup(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserGroup> CreateGroup(string groupName)
        {
            var mn = new UserGroup(groupName,null);
           await procurementdbcontext.UserGroups.AddAsync(mn);

          await  procurementdbcontext.SaveChangesAsync();

            return mn;
        }

      
        public async Task DeleteGroup(long grpId)
        {
          var ug = await  procurementdbcontext.UserGroups.Include(x => x.UserRole).FirstOrDefaultAsync(x => x.Id == grpId);

            if (ug == null)
            {
                throw new KeyNotFoundException("Group not found");
            }



            if (ug.UserRole != null)
            {
                var role = await _roleManager.FindByIdAsync(ug.UserRoleId);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                }
            }                      
           
             procurementdbcontext.Remove(ug);

            await procurementdbcontext.SaveChangesAsync();
        }

        public async Task<IList<UserGroup>> GetAll()
        {
            return await procurementdbcontext.UserGroups.ToListAsync();
        }

        public async Task<IList<GroupUsersViewModel>> GetAllUsersInGroup(int id)
        {
            var grp = await procurementdbcontext.UserGroups.Include(x => x.Staffs).ThenInclude(x=> x.Staff).FirstOrDefaultAsync(x => x.Id == id);

            if (grp == null)
            {
                throw new KeyNotFoundException("Group not found");
            }

           return grp.Staffs.Select(x => new GroupUsersViewModel { Staff = x.Staff, GroupId = id }).ToList();
        }

        public async Task<UserGroup> GetById(long id)
        {
           var mn = await procurementdbcontext.UserGroups.Include(x => x.Staffs).ThenInclude(x => x.Staff).FirstOrDefaultAsync( x=> x.Id == id);          
            return mn;
        }

        public async Task RemoveUserFromGroupAsync(string userId, int groupId)
        {
            //get  role in group
           var grp = await procurementdbcontext.UserGroups.Include(x => x.UserRole).Include(x=> x.Staffs).ThenInclude(x => x.Staff).FirstOrDefaultAsync(x => x.Id == groupId);

            var grpRole = grp?.UserRole;

            var grpRoleName = grpRole?.Name;

            var user = await procurementdbcontext.Staffs.FirstOrDefaultAsync(x => x.Id == userId);

            if ( user != null)
            {

                //remove user from group
               var stgp = await procurementdbcontext.StaffUserGroups.FirstOrDefaultAsync(x => x.StaffId == userId && x.UserGroupId == groupId);

                procurementdbcontext.Remove(stgp);

                if (grpRole != null)
                {
                    //remove user from role
                   await _userManager.RemoveFromRoleAsync(user, grpRoleName);
                }


               await procurementdbcontext.SaveChangesAsync();
            }


        }

        public async Task<List<RazorPagesControllerInfo>> GetRolesInGroup(int groupId)
        {
            var grp = await procurementdbcontext.UserGroups.Include(x => x.UserRole).FirstOrDefaultAsync(x => x.Id == groupId);

            return JsonConvert.DeserializeObject<List<RazorPagesControllerInfo>>(grp?.UserRole.Access);
        }

        public async Task ClearGroupRolesAsync(int groupId)
        {
            var grp = await procurementdbcontext.UserGroups.Include(x => x.UserRole).Include(x => x.Staffs).FirstOrDefaultAsync(x => x.Id == groupId);

            if (grp != null )
            {
                if (grp.UserRole != null)
                {

                    var role = await procurementdbcontext.Roles.FindAsync(grp.UserRole.Id);
                    procurementdbcontext.Remove(role);
                    await procurementdbcontext.SaveChangesAsync();
                }

            }
        }

        public async Task RemoveRoleFromGroup(int groupId, string roleId)
        {
            var grp = await procurementdbcontext.UserGroups.Include(x=> x.UserRole).FirstOrDefaultAsync(x=> x.Id == groupId);

            if (grp == null )
            {
                throw new KeyNotFoundException("group not found");
            }
            else if ( grp.UserRole == null)
            {
                throw new KeyNotFoundException("User role Not found");
            }

            var savedPages = JsonConvert.DeserializeObject<List<RazorPagesControllerInfo>>(grp.UserRole.Access);

            savedPages.RemoveAll(x => x.Id == roleId);
           grp.UserRole.Access = JsonConvert.SerializeObject(savedPages);
            await  procurementdbcontext.SaveChangesAsync();

        }
    }
}
