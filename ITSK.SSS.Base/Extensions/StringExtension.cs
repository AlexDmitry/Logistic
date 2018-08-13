using System;
using ITSK.SSS.Base.Serialization;

namespace ITSK.SSS.Base.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool NotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }

        public static string Template(this string format, params object[] ars)
        {
            return string.Format(format, ars);
        }
        
        public static string SubstringWithDots(this string str, int length)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            return str.Length > length ? str.Substring(0, length) + "..." : str;
        }

        public static string CorrectStringValue(this string str)
        {
            return !string.IsNullOrEmpty(str)
                ? str.Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r")
                : string.Empty;
        }

        public static string CorrectCharracterType(this string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : str.Replace("<", "&lt;").Replace(">", "&gt;").Replace('"', '\'');
        }

        public static T ConverXml<T>(this string str, Type[] knowTypes = null)
        {
            return string.IsNullOrEmpty(str) ? default(T) : XmlSerializer.Deserialize<T>(str, knowTypes);
        }

        public static string ToJsString(this string el)
        {
            return string.IsNullOrEmpty(el) ? string.Empty : el.Replace("\"", "\\\"");
        }
    }
}