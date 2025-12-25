using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.Common
{
    internal class PasswordHash
    {
        public string CreatePasswordHash(string pw)
        {
            const string salt = @"pbkdf2s@ltkeyt0h@sh";
            var pwHash = "";
            var encoder = new UTF8Encoding();

            var b = new Rfc2898DeriveBytes(pw, encoder.GetBytes(salt), 10000);
            var k = b.GetBytes(32);
            pwHash = Convert.ToBase64String(k);
            return pwHash;
        }
    }
}
