using System;
using System.Collections.Generic;
using System.Linq;
using rossum.Answering;
using rossum.Files;
using rossum.Machine.Reading.Reworders;
using rossum.Reading.Readers;
using rossum.Settings;

namespace rossum.Machine.Reading.Tokenizers
{
    public class TFIDF : ITokenizer
    {
        Dictionary<string, double> _idf = new Dictionary<string, double>();

        public TFIDF(string filePath1, string filePath2, IReworder reworder, IReader reader, bool train)
        {
            Console.Write(Environment.NewLine + "Preparing IDF");
            int linesRead = 0;

            foreach (string line in LinesEnumerator.YieldLines(filePath1))
            {
                List<string> res = reader.Read(ReworderHelper.Map(line, reworder)).Split(' ').ToList();

                foreach (string element in res.Distinct())
                {
                    if (_idf.ContainsKey(element))
                        _idf[element]++;
                    else
                        _idf.Add(element, 1);
                }

                if ((linesRead % DisplaySettings.PrintProgressEveryLine) == 0)
                    Console.Write('.');

                linesRead++;
            }

            foreach (string line in LinesEnumerator.YieldLines(filePath2))
            {
                RawQuestion rq = new RawQuestion(line, train);
                string[] combinations = rq.GetCombinations();
                for (int i = 0; i < combinations.Length; i++)
                    foreach (string element in reader.Read(ReworderHelper.Map(combinations[i], reworder)).Split(' ').Distinct())
                    {
                        if (_idf.ContainsKey(element))
                            _idf[element]++;
                        else
                            _idf.Add(element, 1);
                    }
                if ((linesRead % DisplaySettings.PrintProgressEveryLine) == 0)
                    Console.Write('.');

                linesRead++;
            }

            int n = _idf.Count;

            string[] originalKeys = _idf.Keys.ToArray();

            foreach (string key in originalKeys)
                _idf[key] = Math.Log(n * 1f / _idf[key]);
        }

        public IDictionary<string, double> Tokenize(string line)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();

            string[] splittedLine = line.Split(' ');

            foreach (string elt in splittedLine)
            {
                double idf = 0;
                _idf.TryGetValue(elt, out idf);
                if (res.ContainsKey(elt))
                    res[elt] += idf;
                else
                    res.Add(elt, idf);
            }
            return res;
        }
    }
}
