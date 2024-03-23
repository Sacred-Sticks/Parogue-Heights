using System.Collections.Generic;

namespace Parogue_Heights
{
    public static class GenericRegistry<TKey, TValue>
    {
        public static Dictionary<TKey, TValue> Registry = new Dictionary<TKey, TValue>();

        public static void Register(TKey key, TValue value)
            => Registry.Add(key, value);

        public static void Deregister(TKey key)
            => Registry.Remove(key);

    }
}
