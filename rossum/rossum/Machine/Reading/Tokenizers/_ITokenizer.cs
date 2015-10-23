using System.Collections.Generic;

namespace rossum.Machine.Reading.Tokenizers
{
    public interface ITokenizer
    {
        IDictionary<string, double> Tokenize(string line);
    }
}
