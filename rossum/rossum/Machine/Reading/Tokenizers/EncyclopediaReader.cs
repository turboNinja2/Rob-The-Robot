using System;
using System.Collections.Generic;
using rossum.Files;
using rossum.Machine.Reading.Reworders;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;
using rossum.Settings;

namespace rossum.Reading
{
    public static class EncyclopediaReader
    {
        public static IDictionary<string, double>[] ImportSparse(string filePath, IReworder reworder, IReader reader, ITokenizer tokenizer)
        {
            List<IDictionary<string, double>> encyclopedia = new List<IDictionary<string, double>>();
            int linesRead = 0;

            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                IDictionary<string, double> res = tokenizer.Tokenize(reader.Read(reworder.Map(line)));

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
