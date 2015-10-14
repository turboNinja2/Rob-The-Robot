using System.Collections.Generic;

namespace rossum.Learning.SparseKernels
{
    interface ISparseKernel
    {
        public double Value(Dictionary<string, double> p1, Dictionary<string, double> p2);
    }
}
