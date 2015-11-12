using System;
using System.Linq;
using System.Text.RegularExpressions;
using Iveonik.Stemmers;
using rossum.Machine.Reading;

namespace rossum.Reading.Readers
{
    public class StemPunctuationY : IReader
    {
        IStemmer englishStemmer = new EnglishStemmer();

        public string Read(string line)
        {
            StemPunctuation preRead = new StemPunctuation();
            line = preRead.Read(line);

            line = line.Replace('y', 'i');
            Regex multipleIs = new Regex("i+");
            line = multipleIs.Replace(line, "i");

            return line;
        }
    }
}
