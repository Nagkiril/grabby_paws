using System.Collections.Generic;
using UnityEngine;

namespace IV.ListExtensions
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> targetList)
        {
            T randomValue = default(T);
            if (!targetList.IsNullOrEmpty())
            {
                randomValue = targetList[Random.Range(0, targetList.Count)];
            }

            return randomValue;
        }

        public static bool IsNullOrEmpty<T>(this List<T> targetList)
        {
            return targetList == null || targetList.Count == 0;
        }
    }
}