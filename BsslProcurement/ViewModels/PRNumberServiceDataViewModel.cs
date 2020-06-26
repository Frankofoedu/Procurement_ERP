using System.Collections.Generic;

namespace BsslProcurement.ViewModels
{
    public class PRNumberServiceDataViewModel
    {
        public string prnumber { get; set; }
        public string requestDeptCode { get; set; }
        public string requestDept { get; set; }
        public List<string> departments { get; set; }
    }
}
