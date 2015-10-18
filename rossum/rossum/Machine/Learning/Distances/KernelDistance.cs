using System.Collections.Generic;
using rossum.Learning.SparseKernels;

namespace rossum.Machine.Learning.SparseDistances
{
    class KernelDistance : ISparseDistance
    {
        private ISparseKernel _kernel;

        public KernelDistance(ISparseKernel kernel)
        {
            _kernel = kernel;
        }

        public double Value(IDictionary<string, double> p1, IDictionary<string, double> p2)
        {
            return 0;// _kernel.Value(p1, p1) + _kernel.Value(p2, p2) - 2 * _kernel.Value(p1, p2);
        }
    }
}
