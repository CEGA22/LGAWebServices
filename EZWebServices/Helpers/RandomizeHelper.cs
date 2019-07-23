using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EZWebServices.Helpers
{
    public class RandomizeHelper
    {
        private static Random random = new Random();
        public static string GenerateRandomString(int length = 8)
        {
            const string chars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}