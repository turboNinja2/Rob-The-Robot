using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning.SparseDistances
{
    class NormalizedJaccard : ISparseDistance
    {
        public double Value(IDictionary<string, double> p1, IDictionary<string, double> p2)
        {
            int unionSize = 0,
                intersectionSize = 0;

            string[] keys = p2.Keys.ToArray();

            foreach (string key in keys)
                if (p1.ContainsKey(key))
                    intersectionSize++;

            unionSize = p1.Count + p2.Count - intersectionSize;
            return 1 - intersectionSize * 1f / unionSize;
        }
    }
}
