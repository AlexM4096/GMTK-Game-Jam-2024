using System;
using UnityEngine;

namespace AlexTools.Extensions
{
    public static class NumberExtensions 
    {
        public static float DivideBy(this int part, int whole)
        {    
            if (whole == 0) return float.NaN;
            return (float) part / whole;
        }
        
        public static bool IsOdd(this int i) => i % 2 == 1;
        public static bool IsEven(this int i) => i % 2 == 0;

        public static int AtLeast(this int value, int min) => Mathf.Max(value, min);
        public static int AtMost(this int value, int max) => Mathf.Min(value, max);

        public static float AtLeast(this float value, float min) => Mathf.Max(value, min);
        public static float AtMost(this float value, float max) => Mathf.Min(value, max);

        public static double AtLeast(this double value, double min) => Math.Max(value, min);
        public static double AtMost(this double value, double min) => Math.Min(value, min);
        
        public static decimal AtLeast(this decimal value, decimal min) => Math.Max(value, min);
        public static decimal AtMost(this decimal value, decimal min) => Math.Min(value, min);
    }
}