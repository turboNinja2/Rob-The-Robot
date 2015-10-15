using System;
using System.Collections.Generic;
using rossum.Files;
using rossum.Reading.Readers;
using rossum.Settings;
using rossum.Tools;

namespace rossum.Reading
{
    public static class EncyclopediaReader
    {
        public static Dictionary<string, double>[] Import(string filePath, IReader reader)
        {
            List<Dictionary<string, double>> encyclopedia = new List<Dictionary<string,double>>();
            int linesRead = 0;

            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                Dictionary<string, double> res = TextToData.Counts(reader.Read(line));

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
