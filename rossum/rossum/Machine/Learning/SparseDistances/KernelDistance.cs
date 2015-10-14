using rossum.Learning.SparseKernels;
using System.Collections.Generic;

namespace rossum.Learning.SparseDistances
{
    class KernelDistance : ISparseDistance
    {
        private ISparseKernel _kernel;

        public KernelDistance(ISparseKernel kernel)
        {
            _kernel = kernel;
        }

        public double Value(Dictionary<string, double> p1, Dictionary<string, double> p2)
        {
            return _kernel.Value(p1, p1) + _kernel.Value(p2, p2) - 2 * _kernel.Value(p1, p2);
        }
    }
}
