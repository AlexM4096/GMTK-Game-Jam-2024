using System;
using System.Collections;
using System.Collections.Generic;

namespace AlexTools.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
                action(element);
        }
        
        public static void ForEach(this IEnumerable enumerable, Action<object> action)
        {
            foreach (var element in enumerable)
                action(element);
        }
    }
}