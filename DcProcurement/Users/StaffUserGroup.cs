using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement.Users
{
    public class StaffUserGroup
    {
        public string StaffId { get; set; }
        public Staff Staff { get; set; }

        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}
