using DcProcurement;
using DcProcurement.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.ViewModels
{
    public class VendorWithEmailViewModel
    {
        public List<VendorWithEmailObject> VendorWithEmailList { get; set; }

        public List<VendorWithEmailObject> GetVendorWithEmailList(List<Accust> vendors )
        {
            var vendorList = new List<VendorWithEmailObject>();

            foreach (var item in vendors)
            {
                var vObj = new VendorWithEmailObject() {
                    Email = item.Memail,
                    Id = item.Keyid.ToString(),
                    isSelected = false,
                    Name=item.Accname,
                    Number = item.Custcode
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
