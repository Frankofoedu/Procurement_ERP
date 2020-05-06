using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Filters.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class NoDiscoveryAttribute : Attribute
    {
    }
}
