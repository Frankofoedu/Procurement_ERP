using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Services
{
    public class GroupManagementService : IGroupManagement
    {
        private readonly ProcurementDBContext procurementdbcontext;

        public GroupManagementService(ProcurementDBContext _procurementdbcontext)
        {
            procurementdbcontext = _procurementdbcontext;
        }

        public void AddListUserToGroup(List<string> userIds, int groupId)
        {
            if (userIds != null && userIds.Count> 0)
            {
                var staffs = procurementdbcontext.Staffs.Where(x=> userIds.Contains(x.Id));

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
          var ug =  procurementdbcontext.UserGroups.Find(grpId);

            if (ug == null)
            {
                throw new KeyNotFoundException("Group not found");
            }

            procurementdbcontext.UserGroups.Remove(ug);

           await procurementdbcontext.SaveChangesAsync();
        }

        public async Task<IList<UserGroup>> GetAll()
        {
            return await procurementdbcontext.UserGroups.ToListAsync();
        }

        public async Task<IList<GroupUsersViewModel>> GetAllUsersInGroup(int id)
        {
            var grp = await procurementdbcontext.UserGroups.Include(x => x.Staffs).FirstOrDefaultAsync(x => x.Id == id);

            if (grp == null)
            {
                throw new KeyNotFoundException("Group not found");
            }

           return grp.Staffs.Select(x => new GroupUsersViewModel { Staff = x, GroupId = id }).ToList();
        }

        public async Task<UserGroup> GetById(long id)
        {
           var mn = await procurementdbcontext.UserGroups.FindAsync(id);          
            return mn;
        }

        public void RemoveUserFromGroup(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
