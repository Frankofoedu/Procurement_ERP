using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DcProcurement.Users
{
    public class UserRole: IdentityRole
    {
        public string Access { get; set; }
    }
}
