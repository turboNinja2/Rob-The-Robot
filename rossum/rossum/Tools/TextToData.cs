using System.Collections.Generic;
using rossum.Files;

namespace rossum.Tools
{
    public static class TextToData
    {
        public static Dictionary<string, double> Counts(string line)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();

            foreach (string elt in line.Split(' '))
            {
                if (res.ContainsKey(elt))
                {
                    res[elt]++;
                }
                else
                {
                    res.Add(elt, 1);
                }
            }

            return res;
        }

        public static string[] ImportIds(string filePath)
        {
            List<string> res = new List<string>();
            int linesRead = 0;
            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                linesRead++;
                if (linesRead == 1) continue;
                res.Add(line.Split('\t')[0]);

            }
            return res.ToArray();
        }
    }
}
