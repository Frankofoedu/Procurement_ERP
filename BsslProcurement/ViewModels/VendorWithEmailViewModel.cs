using DcProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.ViewModels
{
    public class VendorWithEmailViewModel
    {
        public List<VendorWithEmailObject> VendorWithEmailList { get; set; }

        public List<VendorWithEmailObject> GetVendorWithEmailList(List<VendorUser> vendorUsers )
        {
            var vendorList = new List<VendorWithEmailObject>();

            foreach (var item in vendorUsers)
            {
                var vObj = new VendorWithEmailObject() {
                    Email = item.Email,
                    Id = item.Id,
                    isSelected = false,
                    Name=item.CompanyInfo.CompanyName,
                    Number = item.CompanyInfo.CompanyRegNo
                };

                vendorList.Add(vObj);
            }
            return vendorList;
        }
    }
    public class VendorWithEmailObject
    {
        public string Id { get; set; }
        public bool isSelected { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
