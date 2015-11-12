using System;
using System.Linq;
using Iveonik.Stemmers;
using rossum.Machine.Reading;
using System.Text.RegularExpressions;

namespace rossum.Reading.Readers
{
    public class StemPunctuationNumber : IReader
    {
        IStemmer englishStemmer = new EnglishStemmer();

        public string Read(string line)
        {
            line = StringHelper.RemovePunctuation(line);

            Regex numbers = new Regex("[0-9]+");
            line = numbers.Replace(line, "num");

            line = String.Join(" ", line.Split(' ').Select(c => englishStemmer.Stem(c)));
            return line;
        }
    }
}
