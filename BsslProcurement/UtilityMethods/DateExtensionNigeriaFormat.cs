using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.UtilityMethods
{
    public static class DateExtensionNigeriaFormat
    {
        public static string ToShortNigeriaDateString(this DateTime date)
        {
            //return date.ToString("dd/MM/yyyy hh:mm tt");
            return date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static DateTime ToNigeriaDate(this string date)
        {
            try
            {
                var dateonly = date.Split(' ').First();
                var splitDate = dateonly.Split('/');

                var dDate = new DateTime(int.Parse(splitDate[2], CultureInfo.InvariantCulture), int.Parse(splitDate[1], CultureInfo.InvariantCulture), int.Parse(splitDate[0], CultureInfo.InvariantCulture));

                return dDate;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
