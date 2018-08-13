using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace ITSK.SSS.Base.Serialization
{
    public class XmlSerializer
    {
        /// <summary>
        /// Xml Serialization to json string
        /// </summary>
        public static string Serialize<T>(T obj, Type[] knowTypes = null)
        {
            string xmlString;
            try
            {
                var types = new List<Type> { typeof(T) };
                if (knowTypes != null)
                {
                    types.AddRange(knowTypes);
                }

                var serializer = new DataContractSerializer(obj.GetType(), types);
                using (var ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    xmlString = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception)
            {
                xmlString = string.Empty;
            }

            return xmlString;
        }

        /// <summary>
        /// Xml Deserialization
        /// </summary>
        public static T Deserialize<T>(string xmlString, Type[] knowTypes = null)
        {
            var types = new List<Type> { typeof(T) };
            if (knowTypes != null)
            {
                types.AddRange(knowTypes);
            }

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                var deserializer = new DataContractSerializer(typeof(T), types);
                return (T)deserializer.ReadObject(ms);
            }
        }
    }
}
