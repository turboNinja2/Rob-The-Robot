using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning.SparseDistances
{
    class Tanimoto : ISparseDistance
    {
        public double Value(IDictionary<string, double> A, IDictionary<string, double> B)
        {
            double dot = 0;

            string[] keys = B.Keys.ToArray();

            foreach (string key in keys)
                if (A.ContainsKey(key))
                    dot += A[key] * B[key];

            double ssqA = A.Select(c => c.Value).Sum(c => c * c);
            double ssqB = B.Select(c => c.Value).Sum(c => c * c);

            return -dot / (ssqA + ssqB - dot);
        }
    }
}
