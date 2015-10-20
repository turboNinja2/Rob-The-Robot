using System;
using System.Collections.Generic;
using System.Linq;

namespace rossum.Machine.Learning.SparseDistances
{
    /// <summary>
    /// Tells us the number of edits needed to turn one string into another.
    /// Source: http://www.dotnetperls.com/levenshtein (+ some edits)
    /// </summary>
    public class Levenshtein : ISparseDistance
    {
        public double Value(IDictionary<string, double> p1, IDictionary<string, double> p2)
        {
            string[] s = p1.Keys.ToArray(),
                t = p2.Keys.ToArray();

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m] * 1f / Math.Max(t.Length, s.Length);

        }
    }
}
