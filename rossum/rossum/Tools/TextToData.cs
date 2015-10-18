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
                if (res.ContainsKey(elt))
                    res[elt]++;
                else
                    res.Add(elt, 1);
            return res;
        }

        public static Dictionary<string, double> Order(string line)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();
            int index = 0;
            foreach (string elt in line.Split(' '))
            {
                res.Add(elt, index);
                index++;
            }
            return res;
        }

        public static string[] ImportColumn(string filePath, int colIndex)
        {
            List<string> res = new List<string>();
            int linesRead = 0;
            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                linesRead++;
                if (linesRead == 1) continue;
                res.Add(line.Split('\t')[colIndex]);
            }
            return res.ToArray();
        }
    }
}
