using System;
using System.Collections.Generic;
using rossum.Answering;
using rossum.Files;
using rossum.Settings;

namespace rossum.Reading.Readers
{
    public static class QuestionnaireReader
    {
        public static RawQuestion[] Import(string filePath, bool train)
        {
            List<RawQuestion> questions = new List<RawQuestion>();
            int linesRead = 0;
            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                linesRead++;
                if (linesRead == 1) continue;
                questions.Add(new RawQuestion(line, train));

                if ((linesRead % DisplaySettings.PrintProgressEveryLine) == 0)
                {
                    Console.Write('.');
                }
            }
            return questions.ToArray();
        }
    }
}
