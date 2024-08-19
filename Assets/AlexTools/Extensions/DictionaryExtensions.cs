using System.Collections.Generic;

namespace AlexTools.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue AsFunc<TKey, TValue>(this Dictionary<TKey,
            TValue> dictionary, TKey key) => dictionary[key];
        
        public static TValue AsFunc<TKey, TValue>(this IDictionary<TKey,
            TValue> dictionary, TKey key) => dictionary[key];
        
        public static TValue AsFunc<TKey, TValue>(this IReadOnlyDictionary<TKey,
            TValue> dictionary, TKey key) => dictionary[key];
    }
}