using System;
using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning.SparseDistances
{
    class CosineDistance : ISparseDistance
    {
        public double Value(IDictionary<string, double> p1, IDictionary<string, double> p2)
        {
            double dot = 0;

            string[] keys = p2.Keys.ToArray();

            foreach (string key in keys)
                if (p1.ContainsKey(key))
                    dot += p1[key] * p2[key];

            return -dot / Math.Sqrt(p1.Sum(c => c.Value * c.Value)) * Math.Sqrt(p2.Sum(c => c.Value * c.Value));
        }
    }
}
