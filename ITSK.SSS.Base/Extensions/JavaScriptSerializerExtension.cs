using System.Collections.Generic;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace ITSK.SSS.Base.Extensions
{
    public static class JavaScriptSerializerExtension
    {
        public static dynamic DeserializeDynamic(this JavaScriptSerializer serializer, string value)
        {
            var dictionary = serializer.Deserialize<IDictionary<string, object>>(value);
            return GetExpando(dictionary);
        }

        private static ExpandoObject GetExpando(IDictionary<string, object> dictionary)
        {
            var expando = (IDictionary<string, object>)new ExpandoObject();

            foreach (var item in dictionary)
            {
                var innerDictionary = item.Value as IDictionary<string, object>;
                if (innerDictionary != null)
                {
                    expando.Add(item.Key, GetExpando(innerDictionary));
                }
                else
                {
                    expando.Add(item.Key, item.Value);
                }
            }

            return (ExpandoObject)expando;
        }

        public static string SerializeToJson(this object target)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(target);
        }

        public static T DeserializeFromJson<T>(this string target)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(target);
        }
    }
}
