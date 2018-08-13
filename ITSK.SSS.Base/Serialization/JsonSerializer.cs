using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ITSK.SSS.Base.Serialization
{
    public class JsonSerializer
    {
        /// <summary>
        /// JSON Serialization to json string
        /// </summary>
        public static string Serialize<T>(T obj, Type[] knowTypes = null)
        {
            string jsonString;
            try
            {
                var types = new List<Type> { typeof(T) };
                if (knowTypes != null)
                {
                    types.AddRange(knowTypes);
                }

                var serializer = new DataContractJsonSerializer(obj.GetType(), types);
                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    jsonString = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception)
            {
                jsonString = string.Empty;
            }

            return jsonString;
        }

        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T Deserialize<T>(string jsonString, Type[] knowTypes = null)
        {
            var types = new List<Type> { typeof(T) };
            if (knowTypes != null)
            {
                types.AddRange(knowTypes);
            }

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                var deserializer = new DataContractJsonSerializer(typeof(T),
                    new DataContractJsonSerializerSettings
                    {
                        DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ss+00:00"),
                        KnownTypes = types
                    });
                return (T)deserializer.ReadObject(ms);
            }
        }
    }
}
