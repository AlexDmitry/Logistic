using System;
using System.Xml.Linq;

namespace ITSK.SSS.Base.Extensions
{
    public static class XElementExtention
    {
        public static string ToJsString(this XElement el)
        {
            return el.ToString(SaveOptions.DisableFormatting).Replace("\"", "\\\"");
        }
    }
}
