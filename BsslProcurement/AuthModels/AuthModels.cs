using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.AuthModels
{
    public class RazorPagesControllerInfo
    {
        public string Id => $"{AreaName}:{DisplayName}";

        public string DisplayName { get; set; }

        public string AreaName { get; set; }
       
    }

  
}
