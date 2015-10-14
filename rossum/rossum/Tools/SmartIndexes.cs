﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace rossum.Tools
{
    /// <summary>
    /// Implements various helpers to access data.
    /// </summary>
    public static class SmartIndexes
    {
        /// <summary>
        /// Returns an inverted dictionnary of the keys.
        /// </summary>
        /// <param name="sample"></param>
        public static Dictionary<T, int[]> InverseKeys<T>(Dictionary<T, double>[] sample, double minValue, int preAlloc1 = 1000000, int preAlloc2 = 100)
        {
            Dictionary<T, List<int>> invertedIndexes = new Dictionary<T, List<int>>(preAlloc1);
            for (int i = 0; i < sample.Length; i++)
            {
                Dictionary<T, double> sparsePoint = sample[i];
                foreach (KeyValuePair<T, double> kvp in sparsePoint)
                {
                    T currentKey = kvp.Key;
                    if (kvp.Value < minValue) continue;
                    if (invertedIndexes.ContainsKey(currentKey))
                        invertedIndexes[currentKey].Add(i);
                    else
                    {
                        invertedIndexes.Add(currentKey, new List<int>(preAlloc2));
                        invertedIndexes[currentKey].Add(i);
                    }
                }
            }

            Dictionary<T, int[]> invertedIndexesArray = new Dictionary<T, int[]>(invertedIndexes.Count); // this dictionnary enjoys a better pre-allocation
            for (int i = 0; i < invertedIndexes.Count; i++)
                invertedIndexesArray.Add(invertedIndexes.ElementAt(i).Key, invertedIndexes.ElementAt(i).Value.ToArray());

            return invertedIndexesArray;
        }

        public static Dictionary<T, int[]> InverseKeysAndSort<T>(Dictionary<T, double>[] sample, double minValue = 0, int preAlloc1 = 1000000, int preAlloc2 = 100)
        {
            Dictionary<T, int[]> invertedIndexes = InverseKeys<T>(sample, minValue, preAlloc1, preAlloc2);
            for (int i = 0; i < invertedIndexes.Count; i++)
                Array.Sort(invertedIndexes[invertedIndexes.Keys.ElementAt(i)]);
            return invertedIndexes;
        }

        /// <summary>
        /// Returns the elements at the specified indexes. 
        /// </summary>
        /// <typeparam name="T">Type of the elements</typeparam>
        /// <param name="source">The array to retrieve the data from</param>
        /// <param name="indexes">The indexes to look up</param>
        /// <returns>The extraction T_indexes_i</returns>
        public static T[] GetElementsAt<T>(T[] source, int[] indexes)
        {
            T[] result = new T[indexes.Length];
            for (int i = 0; i < indexes.Length; i++)
                result[i] = source[indexes[i]];
            return result;
        }
    }

}