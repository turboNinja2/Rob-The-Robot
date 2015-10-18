using System;
using System.Linq;
using Iveonik.Stemmers;

namespace rossum.Reading.Readers
{
    public class EnglishStemmingPunctuation : IReader
    {
        IStemmer englishStemmer = new EnglishStemmer();

        public string Read(string line)
        {
            line = line.Replace(".", "");
            line = line.Replace("_", "");
            line = line.Replace(",", "");
            line = line.Replace("\"", "");

            line = String.Join(" ", line.Split(' ').Select(c => englishStemmer.Stem(c)));
            return line;
        }
    }
}
