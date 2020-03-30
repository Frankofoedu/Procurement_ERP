using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DcProcurement.Users
{
    public class UserGroup
    {
        private UserGroup()
        {

        }
        public UserGroup(string groupName, List<StaffUserGroup> staffs)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentNullException(nameof(groupName));


            GroupName = groupName;

            if (staffs != null)
                if(staffs.Any())
                    Staffs = staffs;



        }

       public void AddRoleToGroup(string roleId)
        {
            UserRoleId = roleId;
        }
        public int Id { get; private set; }
        public string GroupName { get; private set; }

        public string  UserRoleId { get; private set; }
        public UserRole UserRole  { get; private set; }

        public List<StaffUserGroup>  Staffs { get; private set; }
    }
}
