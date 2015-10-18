using System.Collections.Generic;
using rossum.Tools.OrderedDictionary;

namespace rossum.Machine.Reading.Tokenizers
{
    public class Counts : ITokenizer
    {
        public OrderedDictionary<string, double> Tokenize(string line)
        {
            OrderedDictionary<string, double> res = new OrderedDictionary<string, double>();
            foreach (string elt in line.Split(' '))
                if (res.ContainsKey(elt))
                    res[elt]++;
                else
                    res.Add(elt, 1);
            return res;
        }
    }
}
