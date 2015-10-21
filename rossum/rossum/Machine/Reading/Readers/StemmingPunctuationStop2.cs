﻿using System;
using System.Linq;
using Iveonik.Stemmers;
using rossum.Machine.Reading;
using rossum.Machine.Reading.Readers.Stopwords;

namespace rossum.Reading.Readers
{
    public class StemmingPunctuationStop2 : IReader
    {
        IStemmer englishStemmer = new EnglishStemmer();

        public string Read(string line)
        {
            line = StringHelper.RemovePunctuation(line);

            DefaultStopWords sw = new DefaultStopWords();

            line = String.Join(" ", line.Split(' ').Where(c => !sw.Contains(c)).Select(c => englishStemmer.Stem(c)));
            return line;
        }
    }
}