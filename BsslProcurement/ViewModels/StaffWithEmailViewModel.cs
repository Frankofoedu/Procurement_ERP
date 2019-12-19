using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.ViewModels
{
    public class StaffWithEmailViewModel
    {
        public List<StaffWithEmailObject> StaffWithEmailList { get; set; }

        public List<StaffWithEmailObject> GetStaffWithEmailList(List<Staff> staffUsers)
        {
            var staffList = new List<StaffWithEmailObject>();

            foreach (var item in staffUsers)
            {
                var sObj = new StaffWithEmailObject()
                {
                    Email = item.Email,
                    Id = item.Id,
                    isSelected = false,
                    Name = item.Name,
                    Number = item.StaffCode
                };

                staffList.Add(sObj);
            }
            return staffList;
        }
    }

    public class StaffWithEmailObject
    {
        public string Id { get; set; }
        public bool isSelected { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
