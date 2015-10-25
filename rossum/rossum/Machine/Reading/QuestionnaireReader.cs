using System.Collections.Generic;
using rossum.Answering;
using rossum.Files;

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
                if (linesRead == 1) continue; // drop header
                questions.Add(new RawQuestion(line, train));
            }
            return questions.ToArray();
        }
    }
}
