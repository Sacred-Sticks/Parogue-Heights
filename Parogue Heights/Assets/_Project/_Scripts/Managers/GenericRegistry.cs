using System;
using System.Collections.Generic;
using System.Linq;

namespace Parogue_Heights
{
    public static class Registry
    {
        public static Dictionary<object, object> Collection = new Dictionary<object, object>();

        public static void ClearKeyType<TKey>()
            => Collection.Keys
                .Select(k => (TKey) k)
                .ToList()
                .ForEach(k => Collection.Remove(k));

        public static void Register<TValue>(object key, TValue value)
            => Collection.Add(key, value);

        public static void Deregister<TKey>(TKey key)
            => Collection.Remove(key);

        public static TValue Get<TValue>(object key)
        {
            if (Collection.TryGetValue(key, out var value))
                return (TValue) value;
            return default;
        }
    }
}
