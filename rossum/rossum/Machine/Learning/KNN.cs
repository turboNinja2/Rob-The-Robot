using System;
using System.Collections.Generic;
using System.Linq;
using rossum.Tools;

namespace rossum.Machine.Learning
{
    public class SparseKNN<T>
    {
        public delegate double Distance(Dictionary<T, double> sp1, Dictionary<T, double> sp2);

        private const int _INVERTED_INDEXES_PREALLOC_ = 100000;

        #region Private attributes

        private int[] _labels;
        private Dictionary<T, double>[] _points;

        private Dictionary<T, int[]> _invertedIndexes = new Dictionary<T, int[]>();

        private Distance _distance;
        private int _nbNeighbours;

        #endregion

        public SparseKNN(Distance distance, int nbNeighbours, double minTFIDF)
        {
            _distance = distance;
            _nbNeighbours = nbNeighbours;
        }

        /// <summary>
        /// Creates a shallow copy of the data and creates an inverted dictionary.
        /// Be careful, the data is normalized ! Re-import the data after using a KNN (or use it after all the other methods)
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="points"></param>
        public void Train(int[] labels, Dictionary<T, double>[] points)
        {
            _labels = labels;
            _points = points;
            _invertedIndexes = SmartIndexes.InverseKeys(points, 0, _INVERTED_INDEXES_PREALLOC_);
        }

        public double DistanceToClosestPoint(Dictionary<T, double> pt)
        {
            return ClosestDistances(_labels, _points, pt, _nbNeighbours, _distance)[0];
        }

        public string Description()
        {
            return "KNN_k_" + _nbNeighbours + "_dist" + _distance.Method.Name;
        }

        /// <summary>
        /// Returns the labels of the nearest neighbours
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="sample"></param>
        /// <param name="newPoint"></param>
        /// <param name="nbNeighbours"></param>
        /// <returns></returns>
        private double[] ClosestDistances(int[] labels, Dictionary<T, double>[] sample, Dictionary<T, double> newPoint, int nbNeighbours, Distance distance)
        {
            T[] keys = newPoint.Keys.ToArray();

            int[] relevantIndexes = PreselectNeighbours(keys, _invertedIndexes);
            double[] distances = new double[relevantIndexes.Length];

            for (int i = 0; i < relevantIndexes.Length; i++)
            {
                int relevantIndex = relevantIndexes[i];
                distances[i] = distance(newPoint, sample[relevantIndex]);
            }

            double[] neighboursDistances = LazyBubbleSort(distances, nbNeighbours);
            return neighboursDistances;
        }



        /// <summary>
        /// Performs k iterations of the bubble sort algorithm 
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="distances"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static double[] LazyBubbleSort(double[] distances, int k)
        {
            double[] result = new double[k];

            int n = distances.Length;
            for (int j = 0; j < k; j++)
                for (int i = n - 2; i >= 0; i--)
                    if (distances[i] > distances[i + 1])
                    {
                        double distanceTmp = distances[i + 1];
                        distances[i + 1] = distances[i];
                        distances[i] = distanceTmp;
                    }

            Array.Copy(distances, 0, result, 0, Math.Min(distances.Length, k));
            return result;
        }

        /// <summary>
        /// Given keywords, returns the line indexes containing at least one of the keywords.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        private static int[] PreselectNeighbours(T[] keywords, Dictionary<T, int[]> invertedIndexes)
        {
            List<int> candidateIndexes = new List<int>();
            for (int i = 0; i < keywords.Length; i++)
                if (invertedIndexes.ContainsKey(keywords[i]))
                    candidateIndexes.AddRange(invertedIndexes[keywords[i]]);
            return candidateIndexes.Distinct().ToArray();

        }
    }
}