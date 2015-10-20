using System.Collections.Generic;
using rossum.Tools.OrderedDictionary;

namespace rossum.Machine.Reading.Tokenizers
{
    public interface ITokenizer
    {
        IDictionary<string, double> Tokenize(string line);
    }
}
