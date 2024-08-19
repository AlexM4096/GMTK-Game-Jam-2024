using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlexTools.Comparers
{
    public class Vector2Comparer : IEqualityComparer<Vector2>
    {
        public bool Equals(Vector2 a, Vector2 b) 
            => Mathf.Approximately(0, Vector2.Distance(a, b));

        public int GetHashCode(Vector2 obj) 
            => HashCode.Combine(obj.x, obj.y);
    }
}