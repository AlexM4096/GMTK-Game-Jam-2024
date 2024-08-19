using System.Collections.Generic;
using AlexTools.Comparers;
using UnityEngine;

namespace AlexTools
{
    public static class Waiters
    {
        private const int DictionaryCapacity = 100;
        
        private static readonly float FrameDurationInSeconds = 1f / Application.targetFrameRate;
        
        private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDictionary = 
            new(DictionaryCapacity, new FloatComparer());

        public static WaitForSeconds GetWaitForSeconds(float seconds) 
        {
            if (seconds < FrameDurationInSeconds) return null;

            if (WaitForSecondsDictionary.TryGetValue(seconds, out var value)) 
                return value;
                
            value = new WaitForSeconds(seconds);
            WaitForSecondsDictionary.Add(seconds, value);    
            
            return value;
        }
    }
}