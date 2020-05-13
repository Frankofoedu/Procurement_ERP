using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.UtilityMethods
{
    public static class DateExtensionNigeriaFormat
    {
        public static string ToShortNigeriaDateString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy hh:mm tt");
        }
    }
}
