using System.Collections.Generic;

namespace rossum.Learning.SparseKernels
{
    public interface ISparseKernel
    {
        double Value(Dictionary<string, double> p1, Dictionary<string, double> p2);
    }
}
