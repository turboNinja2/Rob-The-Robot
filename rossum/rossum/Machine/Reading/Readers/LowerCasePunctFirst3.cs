using rossum.Machine.Reading;
using System.Linq;
using System;

namespace rossum.Reading.Readers
{
    public class LowerCasePunctFirst3 : IReader
    {
        public string Read(string line)
        {
            line = StringHelper.RemovePunctuation(line);
            line = String.Join(" ", line.ToLower().Split(' ').Select(c => c.Substring(0, Math.Min(3,c.Length))));
            return line;
        }
    }
}
