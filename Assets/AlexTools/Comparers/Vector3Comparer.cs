using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlexTools.Comparers
{
    public class Vector3Comparer : IEqualityComparer<Vector3>
    {
        public bool Equals(Vector3 a, Vector3 b) 
            => Mathf.Approximately(0, Vector3.Distance(a, b));

        public int GetHashCode(Vector3 obj) 
            => HashCode.Combine(obj.x, obj.y, obj.z);
    }
}