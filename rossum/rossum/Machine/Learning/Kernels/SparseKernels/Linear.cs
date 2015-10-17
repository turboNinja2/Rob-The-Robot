using System.Collections.Generic;

namespace rossum.Learning.SparseKernels
{
    public class Linear : ISparseKernel
    {
        public double Value(Dictionary<string, double> sp1, Dictionary<string, double> sp2)
        {
            double dot = 0;

            foreach (KeyValuePair<string, double> kvp2 in sp2)
                if (sp1.ContainsKey(kvp2.Key))
                    dot += kvp2.Value * sp1[kvp2.Key];

            return dot;
        }
    }
}
