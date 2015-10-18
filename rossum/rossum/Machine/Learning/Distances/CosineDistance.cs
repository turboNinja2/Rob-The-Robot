using System;
using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning.SparseDistances
{
    class CosineDistance : ISparseDistance
    {
        public double Value(IDictionary<string, double> p1, IDictionary<string, double> p2)
        {
            int intersectionSize = 0;

            string[] keys = p2.Keys.ToArray();

            foreach (string key in keys)
                if (p1.ContainsKey(key))
                    intersectionSize++;

            return -intersectionSize * 1f / Math.Sqrt(p1.Count * p2.Count);
        }
    }
}
