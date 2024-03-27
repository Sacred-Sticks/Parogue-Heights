using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Parogue_Heights
{
    public class Registry : MonoBehaviour
    {
        #region UnityEvents
        private void Awake()
        {
            Collection.Clear();
        }
        #endregion

        public static Dictionary<object, object> Collection = new Dictionary<object, object>();

        public static void ClearKeyType<TKey>()
            => Collection.Keys
                .Where(k => k is TKey)
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
