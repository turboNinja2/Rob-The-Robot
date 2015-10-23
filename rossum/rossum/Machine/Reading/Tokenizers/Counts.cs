using System.Collections.Generic;

namespace rossum.Machine.Reading.Tokenizers
{
    public class Counts : ITokenizer
    {
        public IDictionary<string, double> Tokenize(string line)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();
            foreach (string elt in line.Split(' '))
                if (res.ContainsKey(elt))
                    res[elt]++;
                else
                    res.Add(elt, 1);
            return res;
        }
    }
}
