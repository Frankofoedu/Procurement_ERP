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
        public UserGroup(string groupName, List<Staff> staffs)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentNullException(nameof(groupName));


            GroupName = groupName;

            if (staffs != null)
                if(staffs.Any())
                    Staffs = staffs;



        }
        public int Id { get; private set; }
        public string GroupName { get; private set; }

        public List<Staff> Staffs { get; private set; }
    }
}
