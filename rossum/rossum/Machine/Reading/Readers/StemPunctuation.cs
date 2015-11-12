using System;
using System.Linq;
using Iveonik.Stemmers;
using rossum.Machine.Reading;

namespace rossum.Reading.Readers
{
    public class StemPunctuation : IReader
    {
        IStemmer englishStemmer = new EnglishStemmer();

        public string Read(string line)
        {
            line = StringHelper.RemovePunctuation(line);
            line = String.Join(" ", line.Split(' ').Select(c => englishStemmer.Stem(c)));
            return line;
        }
    }
}
