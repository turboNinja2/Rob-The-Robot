using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace rossum.Reading.Readers
{
    public class NaiveLowerCasePunctuation : IReader
    {
        public string Read(string line)
        {
            line = line.Replace(".", "");
            line = line.Replace("_", "");
            line = line.Replace(",", "");

            line = line.ToLower();
            return line;
        }
    }
}
