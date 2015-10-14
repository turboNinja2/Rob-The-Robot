using System;
using System.Collections.Generic;

namespace rossum.Learning.SparseKernels
{
    public class Poly : ISparseKernel
    {
        private int _degree = 1;
        private double _offset = 0;

        public Poly(int degree, double offset)
        {
            _degree = degree;
            _offset = offset;
        }

        public double Value(Dictionary<string, double> sp1, Dictionary<string, double> sp2)
        {
            double dot = 0;

            foreach (KeyValuePair<string, double> kvp2 in sp2)
                if (sp1.ContainsKey(kvp2.Key))
                    dot += kvp2.Value * sp1[kvp2.Key];

            return Math.Pow(dot + _offset, _degree);
        }
    }
}
