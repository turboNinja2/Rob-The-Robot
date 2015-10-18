﻿using System.Collections.Generic;

namespace rossum.Machine.Learning.SparseDistances
{
    public interface ISparseDistance
    {
        double Value(Dictionary<string, double> p1, Dictionary<string, double> p2);
    }
}