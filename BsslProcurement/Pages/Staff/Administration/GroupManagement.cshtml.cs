using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using DcProcurement.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement
{
    public class GroupManagementModel : PageModel
    {
        public string Message { get; set; }
        public string Error { get; set; }

        public List<UserGroupViewModel>  GroupViewModels { get; set; } = new List<UserGroupViewModel>();

        private readonly ProcurementDBContext _procurementDBContext;

        public GroupManagementModel(ProcurementDBContext procurementDBContext)
        {
            _procurementDBContext = procurementDBContext;
        }

        public void OnGet()
        {
            GroupViewModels =_procurementDBContext.UserGroups.Select(x=> new UserGroupViewModel { UserGroup = x }).ToList();
        }
    }

    public class UserGroupViewModel
    {
        public int Index { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}