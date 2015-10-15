using System.Collections.Generic;

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
    }
}
