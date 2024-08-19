using System.Collections.Generic;
using UnityEngine;

namespace AlexTools.Comparers
{
    public class FloatComparer : IEqualityComparer<float> 
    {
        public bool Equals(float x, float y) 
            => Mathf.Approximately(x, y);
        
        public int GetHashCode(float obj) 
            => obj.GetHashCode();
    }
}