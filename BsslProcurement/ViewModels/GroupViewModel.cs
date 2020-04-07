using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GroupUsersViewModel
    {
        public int GroupId { get; set; }
        public DcProcurement.Staff Staff { get; set; }
    }
}
