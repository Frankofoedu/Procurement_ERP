using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.UtilityMethods
{
    public static class PasswordEncrypt
    {
        public static string GetEncryptedPassword(string pwd)
        {
            byte[] passBytes = System.Text.Encoding.Unicode.GetBytes(pwd.ToUpper());
            return Convert.ToBase64String(passBytes);
        }
    }
}
