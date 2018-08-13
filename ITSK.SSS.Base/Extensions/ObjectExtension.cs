using System.Collections.Generic;
using System.Linq;

namespace ITSK.SSS.Base.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Получение значения объекта или указанного значения, 
        /// если объект имеет нулевое значение,
        /// либо его значение равно значению по умолчанию для этого типа объектов
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T GetObject<T>(this T obj, T def)
        {
            if (default(T) == null)
            {
                if (obj == null) return def;
            }
            else
                if (default(T).Equals(obj))
                    return def;
            return obj;
        }

        public static string ToStringSafe(this object target)
        {
            return target != null ? target.ToString() : string.Empty;
        }
    }
}
