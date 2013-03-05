using System;
using System.Linq;
using System.Text;

namespace J.F.Libs.Extensions
{
    public static class ArrayExtensions
    {
        public static string ArrayToString(this Array array, string separator = ",", string itemFormat = "{0}")
        {
            string[] retVal = array.Cast<object>().Select(s => string.Format(itemFormat, s)).ToArray();

            return string.Join(separator, retVal);
        }

        public static string ToUtf8String(this byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            return Encoding.UTF8.GetString(bytes);
        }
    }
}