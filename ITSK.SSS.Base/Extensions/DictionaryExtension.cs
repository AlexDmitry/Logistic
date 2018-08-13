using System.Collections.Generic;
using System.Linq;

namespace ITSK.SSS.Base.Extensions
{
    public static class DictionaryExtension
    {
        public static T TryGetValue<T,TT>(this IDictionary<TT, T> dictionary, TT key)
        {
            T result;
            dictionary.TryGetValue(key, out result);
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null)
                return true;
            return !items.Any();
        }
    }
}