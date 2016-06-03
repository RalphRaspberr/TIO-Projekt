using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class Extensions
    {
        public static string MySHA(this string s)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(s));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }
    }
}
