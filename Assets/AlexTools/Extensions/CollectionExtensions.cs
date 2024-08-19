using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexTools.Extensions
{
    public static class CollectionExtensions
    {
        public static int GetRandomIndex<T>(this ICollection<T> collection) => 
            Random.Range(0, collection.Count);
        public static int GetRandomIndex(this ICollection collection) => 
            Random.Range(0, collection.Count);
        public static int GetRandomIndex<T>(this IReadOnlyCollection<T> collection) => 
            Random.Range(0, collection.Count);

        public static bool IsEmpty<T>(this ICollection<T> collection) => 
            collection.Count == 0;
        public static bool IsEmpty(this ICollection collection) => 
            collection.Count == 0;
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> collection) => 
            collection.Count == 0;
        
        public static bool IsEmptyOrNull<T>(this ICollection<T> collection) => 
            collection == null || collection.IsEmpty();
        public static bool IsEmptyOrNull(this ICollection collection) => 
            collection == null || collection.IsEmpty();
        public static bool IsEmptyOrNull<T>(this IReadOnlyCollection<T> collection) => 
            collection == null || collection.IsEmpty();
    }
}