using System.Collections.Generic;

namespace rossum.Machine.Learning.SparseDistances
{
    public interface ISparseDistance
    {
        double Value(IDictionary<string, double> p1, IDictionary<string, double> p2);
    }
}
