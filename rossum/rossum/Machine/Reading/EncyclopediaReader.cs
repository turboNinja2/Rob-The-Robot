using System.Collections.Generic;
using rossum.Files;
using rossum.Reading.Readers;

namespace rossum.Reading
{
    public static class EncyclopediaReader
    {
        public static Dictionary<string, double>[] Import(string filePath, IReader reader)
        {
            List<Dictionary<string, double>> encyclopedia = new List<Dictionary<string,double>>();
            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                Dictionary<string, double> res = new Dictionary<string, double>();

                foreach (string elt in reader.Read(line).Split(' '))
                    res.Add(elt, 1);

                encyclopedia.Add(res);
            }
            return encyclopedia.ToArray();
        }
    }
}
