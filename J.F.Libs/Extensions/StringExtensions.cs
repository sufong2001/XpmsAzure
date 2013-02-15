using ServiceStack.Text;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace J.F.Libs.Extensions
{
    public static class StringExtensions
    {
        public static TOutput ConvertTo<TOutput>(this string val)
        {
            if (string.IsNullOrEmpty(val)) return default(TOutput);

            Type t = typeof(TOutput);
            Type u = Nullable.GetUnderlyingType(t);
            t = u ?? t;

            if (t.IsEnum) return (TOutput)Enum.Parse(t, val);

            object obj = val;

            if (t == typeof(Boolean)) obj = val.CanBeBooleanTrueString() ? Boolean.TrueString : val;

            return (TOutput)Convert.ChangeType(obj, t);
        }

        public static bool CanBeBooleanTrueString(this string val)
        {
            var convertable = false;

            switch (val.ToLower())
            {
                case "on":
                case "yes":
                    convertable = true;
                    break;
            }

            return convertable;
        }

        public static TOutput[] Split<TOutput>(this string val, char[] separators = null, StringSplitOptions options = StringSplitOptions.None)
        {
            if (string.IsNullOrEmpty(val)) return null;

            if (separators == null || separators.Length < 1) separators = new char[] { ',' };

            string[] retVal = val.Split(separators, options);

            return retVal.Select(s => s.ConvertTo<TOutput>()).ToArray();
        }

        public static string ToJsonArray(this string val)
        {
            if (string.IsNullOrEmpty(val)) return null;

            string[] retVal = val.Split();

            return retVal.ToJson();
        }

        public static string ToJsonArray(this object obj)
        {
            if (obj == null) return null;

            var val = obj.ToString();
            string[] retVal = val.Split();

            return retVal.ToJson();
        }

        public static bool JsonArrayConatins(this string val, string find)
        {
            if (string.IsNullOrEmpty(val)) return false;

            return val.FromJson<string[]>().Any(s => s == find);
        }

        public static T FromJsonString<T>(this string val)
        {
            if (string.IsNullOrEmpty(val)) return default(T);

            return val.FromJson<T>();
        }

        public static string SeperateCamelCase(this string value, string format = " ")
        {
            return Regex.Replace(value, @"([A-Z])(?<=[a-z]\1|[A-Za-z]\1(?=[a-z]))", format + "$1");
        }
    }
}