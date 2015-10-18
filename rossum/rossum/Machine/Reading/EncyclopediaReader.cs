using System;
using System.Collections.Generic;
using rossum.Files;
using rossum.Reading.Readers;
using rossum.Settings;
using rossum.Tools;
using rossum.Machine.Reading.Tokenizers;
using rossum.Tools.OrderedDictionary;
using rossum.Machine.Reading;

namespace rossum.Reading
{
    public static class EncyclopediaReader
    {
        public static OrderedDictionary<string, double>[] ImportSparse(string filePath, IReader reader, ITokenizer tokenizer)
        {
            List<OrderedDictionary<string, double>> encyclopedia = new List<OrderedDictionary<string, double>>();
            int linesRead = 0;

            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                OrderedDictionary<string, double> res = tokenizer.Tokenize(reader.Read(line));

                encyclopedia.Add(res);
                linesRead++;

                if ((linesRead % DisplaySettings.PrintProgressEveryLine) == 0)
                {
                    Console.Write('.');
                }
            }
            return encyclopedia.ToArray();
        }
    }
}
