using System;
using System.Collections;
using System.Collections.Generic;

namespace AlexTools.Extensions
{
    public static class EnumeratorExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator) 
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static Func<bool> AsPredicate(this IEnumerator coroutine) => coroutine.MoveNext;
    }
}