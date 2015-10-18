using System.Collections.Generic;
using rossum.Tools.OrderedDictionary;

namespace rossum.Machine.Reading.Tokenizers
{
    public interface ITokenizer
    {
        OrderedDictionary<string, double> Tokenize(string line);
    }
}
