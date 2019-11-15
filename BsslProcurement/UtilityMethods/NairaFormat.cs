using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.UtilityMethods
{
    public static class NairaFormat
    {
        public static string GetNairaValue(decimal amount)
        {
            return string.Format(new CultureInfo("en-NG", false).NumberFormat, "{0:C}", amount);

        }
    }
}
