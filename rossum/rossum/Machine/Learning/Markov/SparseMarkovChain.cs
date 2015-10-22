using System;
using System.Collections.Generic;

namespace rossum.Machine.Learning.Markov
{
    public class SparseMarkovChain<T>
    {
        private Dictionary<T, Dictionary<T, int>> _sparseMarkovChain = new Dictionary<T, Dictionary<T, int>>();
        private Dictionary<T, int> _countEltLeaving = new Dictionary<T, int>();
        private int _size = 0;

        public double GetTransition(T p1, T p2)
        {
            Dictionary<T, int> p1Value;
            if (!_sparseMarkovChain.TryGetValue(p1, out p1Value))
            {
                return 1f / _size;
            }

            int p2Value;
            if (p1Value.TryGetValue(p2, out p2Value))
            {
                return (1f + p2Value) / (_countEltLeaving[p1] + _size);
            }

            return 1f / (_countEltLeaving[p1] + _size);
        }

        public void AddTransition(T p1, T p2)
        {
            if (_sparseMarkovChain.ContainsKey(p1))
            {
                _countEltLeaving[p1]++;
                if (_sparseMarkovChain[p1].ContainsKey(p2))
                {
                    _sparseMarkovChain[p1][p2] += 1;
                }
                else
                {
                    _sparseMarkovChain[p1].Add(p2, 1);
                }
            }
            else
            {
                _size++;
                if (!_sparseMarkovChain.ContainsKey(p2))
                {
                    _size++;
                }

                _sparseMarkovChain.Add(p1, new Dictionary<T, int> { { p2, 1 } });

                _countEltLeaving.Add(p1, 1);
            }
        }

        public double LogLikelihood(T[] path)
        {
            double res = 0;
            for (int i = 1; i < path.Length; i++)
            {
                res += Math.Log(GetTransition(path[i - 1], path[i]));
            }
            return res;
        }

        public double LengthNormalizedLogLikelihood(T[] path)
        {
            return LogLikelihood(path) / path.Length;
        }

    }
}
