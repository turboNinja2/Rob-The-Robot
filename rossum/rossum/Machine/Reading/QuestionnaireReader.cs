using System.Collections.Generic;
using rossum.Answering;
using rossum.Files;

namespace rossum.Reading.Readers
{
    public static class QuestionnaireReader
    {
        public static Question[] Import(string filePath, IReader reader, bool train)
        {
            List<Question> questions = new List<Question>();
            foreach (string line in LinesEnumerator.YieldLines(filePath))
                questions.Add(new Question(line, train));
            return questions.ToArray();
        }
    }
}
