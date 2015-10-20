using System.Collections.Generic;
using rossum.Files;
using System;

namespace rossum.Tools
{
    public static class TextToData
    {

        public static Dictionary<string, double> ParseString(string line)
        {
            string[] splitted = line.Split(' ');
            Dictionary<string, double> dic = new Dictionary<string, double>();
            foreach (string elt in splitted)
            {
                string key = elt.Split(':')[0];
                double value = Convert.ToDouble(elt.Split(':')[1]);
                dic.Add(key, value);
            }
            return dic;
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
