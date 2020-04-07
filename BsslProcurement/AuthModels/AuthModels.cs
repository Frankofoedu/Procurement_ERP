using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.AuthModels
{
    public class RazorPagesControllerInfo
    {
        public string Id => $"{ViewEnginePath}:{DisplayName}";

        public string DisplayName { get; set; }

        public string ViewEnginePath { get; set; }
       
    }

  
}
