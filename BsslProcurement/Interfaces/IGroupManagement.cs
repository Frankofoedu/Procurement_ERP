using BsslProcurement.AuthModels;
using BsslProcurement.ViewModels;
using DcProcurement.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Interfaces
{
   public interface IGroupManagement
    {

        UserGroup CreateGroup(string groupName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grpId"></param>
        /// <exception cref="KeyNotFoundException">Thrown when no groups are found</exception>
        Task DeleteGroup(long id);

        void AddUserToGroup(string userId);
        void ClearGroupRoles(int groupId);
        void AddListUserToGroup(List<string> userId, int groupId);

        void RemoveUserFromGroup(string userId, int groupId);
        Task<UserGroup> GetById(long id);
        Task<IList<UserGroup>> GetAll();

        Task<IList<GroupUsersViewModel>> GetAllUsersInGroup(int id);
        Task AddUsersInGroupToRole(string roleName, int groupId);
        Task AddRoleToGroup(UserRole role, int groupId);
        Task<List<RazorPagesControllerInfo>> GetRolesInGroup(int groupId);
    }
}
