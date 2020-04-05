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

        public void AddListUserToGroup(List<string> userIds, int groupId)
        {
            if (userIds != null && userIds.Count> 0)
            {
                var staffs = procurementdbcontext.Staffs.Where(x=> userIds.Contains(x.Id)).Select(x => new StaffUserGroup { StaffId = x.Id, UserGroupId = groupId });

                var grp = procurementdbcontext.UserGroups.Include(x => x.Staffs).FirstOrDefault( x => x.Id == groupId);

                if (grp != null)
                {
                    grp.Staffs.AddRange(staffs);

                    procurementdbcontext.SaveChanges();
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

        public UserGroup CreateGroup(string groupName)
        {
            var mn = new UserGroup(groupName,null);
            procurementdbcontext.UserGroups.Add(mn);

            procurementdbcontext.SaveChanges();

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

        public void RemoveUserFromGroup(string userId, int groupId)
        {
            //get  role in group
           var grp =  procurementdbcontext.UserGroups.Include(x => x.UserRole).Include(x=> x.Staffs).ThenInclude(x => x.Staff).FirstOrDefault(x => x.Id == groupId);

            var grpRole = grp?.UserRole;

            var grpRoleName = grpRole?.Name;

            var user = procurementdbcontext.Staffs.FirstOrDefault(x => x.Id == userId);

            if ( user != null)
            {

                //remove user from group
               var stgp = procurementdbcontext.StaffUserGroups.FirstOrDefault(x => x.StaffId == userId && x.UserGroupId == groupId);

                procurementdbcontext.Remove(stgp);

                if (grpRole != null)
                {
                    //remove user from role
                    _userManager.RemoveFromRoleAsync(user, grpRoleName);
                }


                procurementdbcontext.SaveChanges();
            }


        }

        public async Task<List<RazorPagesControllerInfo>> GetRolesInGroup(int groupId)
        {
            var grp = await procurementdbcontext.UserGroups.Include(x => x.UserRole).FirstOrDefaultAsync(x => x.Id == groupId);

            return JsonConvert.DeserializeObject<List<RazorPagesControllerInfo>>(grp?.UserRole.Access);
        }

        public void ClearGroupRoles(int groupId)
        {
            var grp = procurementdbcontext.UserGroups.Include(x => x.UserRole).Include(x => x.Staffs).FirstOrDefault(x => x.Id == groupId);

            if (grp != null )
            {
                if (grp.UserRole != null)
                {

                    var role = procurementdbcontext.Roles.Find(grp.UserRole.Id);
                    procurementdbcontext.Remove(role);
                    procurementdbcontext.SaveChanges();
                }

            }
        }
    }
}
