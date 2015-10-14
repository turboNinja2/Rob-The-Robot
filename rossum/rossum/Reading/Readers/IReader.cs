using System.Collections.Generic;

namespace rossum.Reading.Readers
{
    interface IReader
    {
        public Dictionary<string, double> Read(string input);
    }
}
