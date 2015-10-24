using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning
{

    public class Histogram<T>
    {
        private Dictionary<T, double> _scores = new Dictionary<T, double>(20);
        private const double _EPSILON_ = 1e-5f;

        public Dictionary<T, double> Scores
        {
            get { return _scores; }
        }

        public Histogram()
        {

        }

        public Histogram(int preAlloc)
        {
            _scores = new Dictionary<T, double>(preAlloc);
        }

        public Histogram(Histogram<T> model)
        {
            _scores = new Dictionary<T, double>(model.Scores);
        }

        public Histogram(T[] rawLabels)
        {
            for (int i = 0; i < rawLabels.Length; i++)
                UpdateKey(rawLabels[i], 1);
        }

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

        public void UpdateKey(T key, double value)
        {
            if (_scores.ContainsKey(key))
                _scores[key] += value;
            else
                _scores.Add(key, value);
        }

        public static Histogram<T> Merge(IList<Histogram<T>> empiricScores)
        {
            Histogram<T> result = new Histogram<T>(empiricScores.Select(c => c.Scores.Count).Sum());
            foreach (Histogram<T> empiricScore in empiricScores)
                foreach (KeyValuePair<T, double> kvp in empiricScore.Scores)
                    result.UpdateKey(kvp.Key, kvp.Value);
            return result;
        }
    }
}
