using System.Collections;
using System.Collections.Generic;

namespace AlexTools.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomItem<T>(this IList<T> list) => list[list.GetRandomIndex()];
        public static object GetRandomItem(this IList list) => list[list.GetRandomIndex()];
        public static T GetRandomItem<T>(this IReadOnlyList<T> list) => list[list.GetRandomIndex()];

        public static T PopRandomItem<T>(this IList<T> list)
        {
            int index = list.GetRandomIndex();
            T value = list[index];
            list.RemoveAt(index);
            return value;
        }
        
        public static object PopRandomItem(this IList list)
        {
            int index = list.GetRandomIndex();
            object value = list[index];
            list.RemoveAt(index);
            return value;
        }
        
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int index = list.GetRandomIndex();
                (list[i], list[index]) = (list[index], list[i]);
            }
        }
        
        public static void Shuffle(this IList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int index = list.GetRandomIndex();
                (list[i], list[index]) = (list[index], list[i]);
            }
        }
    }
}