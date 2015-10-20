using System;
using System.Linq;
using Iveonik.Stemmers;

namespace rossum.Reading.Readers
{
    public class Stemming : IReader
    {
        IStemmer englishStemmer = new EnglishStemmer();

        public string Read(string line)
        {
            line = String.Join(" ", line.Split(' ').Select(c => englishStemmer.Stem(c)));
            return line;
        }
    }
}
