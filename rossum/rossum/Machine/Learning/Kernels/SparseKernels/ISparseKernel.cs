using System.Collections.Generic;

namespace rossum.Learning.SparseKernels
{
    public interface IStringKernel
    {
        double Value(string p1, string p2);
    }
}
