using System;
using System.Linq;

namespace J.F.Libs.Extensions
{
    public static class ArrayExtensions
    {
        public static string ArrayToString(this Array array, string separator = ",", string itemFormat = "{0}")
        {
            string[] retVal = array.Cast<object>().Select(s => string.Format(itemFormat, s)).ToArray();

            return string.Join(separator, retVal);
        }
    }
}