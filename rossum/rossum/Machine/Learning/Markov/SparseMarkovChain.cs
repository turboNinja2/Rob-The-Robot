using System.Collections.Generic;

namespace rossum.Machine.Learning.Markov
{
    public class SparseMarkovChain<T>
    {
        // private attributes with default constructor
        private Dictionary<T, Dictionary<T, int>> _sparseMC = new Dictionary<T, Dictionary<T, int>>();
        private Dictionary<T, int> _countEltLeaving = new Dictionary<T, int>();
        private int _size = 0;

        private HashSet<T> _rowsElements = new HashSet<T>();
        private HashSet<T> _colElements = new HashSet<T>();

        public double GetTransition(T p1, T p2)
        {
            if (_sparseMC.ContainsKey(p1))
            {
                if (_sparseMC[p1].ContainsKey(p2))
                    return (1f + _sparseMC[p1][p2]) / (_countEltLeaving[p1] + _size);
                else
                    return 1f / (_countEltLeaving[p1] + _size);
            }
            else
                return 1f / _size; //p1 unknown, return uniform proba
        }

        public void AddTransition(T p1, T p2)
        {
            if (_sparseMC.ContainsKey(p1))
            {
                _countEltLeaving[p1]++;
                if (_sparseMC[p1].ContainsKey(p2))
                    _sparseMC[p1][p2] += 1;
                else
                {
                    _sparseMC[p1].Add(p2, 1); // the default value of the MC is 1, if we see one element, the count value is two
                }
            }
            else
            {
                _size++;
                if (!_colElements.Contains(p2))
                    _size++;
                Dictionary<T, int> nd = new Dictionary<T, int>();
                nd.Add(p2,1);
                _sparseMC.Add(p1, nd);
                _countEltLeaving.Add(p1, 1);
            }
        }
    }
}
