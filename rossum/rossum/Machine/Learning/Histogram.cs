using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning
{
    /// <summary>
    /// Represents the count of a collection of elements of type T.
    /// </summary>
    /// <typeparam name="T">The type of the elements to count.</typeparam>
    public class Histogram<T>
    {
        private Dictionary<T, double> _scores = new Dictionary<T, double>(20);
        private const double _EPSILON_ = 1e-5f;

        public Dictionary<T, double> Scores
        {
            get { return _scores; }
        }

        /// <summary>
        /// Creates an empty histogram.
        /// </summary>
        public Histogram()
        {

        }

        /// <summary>
        /// Creates an empty histogram whose size is pre-allocated.
        /// Can optimized memory accesses.
        /// </summary>
        /// <param name="preAlloc"></param>
        public Histogram(int preAlloc)
        {
            _scores = new Dictionary<T, double>(preAlloc);
        }

        /// <summary>
        /// (Deep) copy constructor.
        /// </summary>
        /// <param name="model"></param>
        public Histogram(Histogram<T> model)
        {
            _scores = new Dictionary<T, double>(model.Scores);
        }

        /// <summary>
        /// Builds an histogram from the counts of the elements of an array.
        /// </summary>
        /// <param name="rawLabels">The input array.</param>
        public Histogram(T[] rawLabels)
        {
            for (int i = 0; i < rawLabels.Length; i++)
                UpdateKey(rawLabels[i], 1);
        }

        /// <summary>
        /// Returns the element with the highest count.
        /// </summary>
        /// <returns></returns>
        public T MostLikelyElement()
        {
            if (_scores.Count == 0) return default(T);

            double bestScore = double.MinValue;
            T mostLikely = _scores.Keys.First();
            foreach (KeyValuePair<T, double> kvp in _scores)
            {
                double currentScore = kvp.Value;
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                    mostLikely = kvp.Key;
                }
            }
            return mostLikely;
        }

        /// <summary>
        /// Updates the count. Creates the key if it is not present.
        /// </summary>
        /// <param name="key">Key of the element to add.</param>
        /// <param name="value">Value to add.</param>
        public void UpdateKey(T key, double value)
        {
            if (_scores.ContainsKey(key))
                _scores[key] += value;
            else
                _scores.Add(key, value);
        }

        /// <summary>
        /// Merges an IList of histogram into one histogram (summing the counts).
        /// </summary>
        /// <param name="empiricScores">Input histograms</param>
        /// <returns>The histogram whose counts are the sum of the counts of the input.</returns>
        public static Histogram<T> Merge(IList<Histogram<T>> empiricScores)
        {
            Histogram<T> result = new Histogram<T>(empiricScores.Select(c => c.Scores.Count).Sum());
            foreach (Histogram<T> empiricScore in empiricScores)
                foreach (KeyValuePair<T, double> kvp in empiricScore.Scores)
                    result.UpdateKey(kvp.Key, kvp.Value);
            return result;
        }

        /// <summary>
        /// Produces a string representation of the histogram : "A:10 B:15 ..."
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return string.Join(" ", _scores.Select(c => c.Key.ToString() + ":" + c.Value));
        }
    }
}
