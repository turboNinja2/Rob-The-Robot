using System;
using rossum.Files;
using rossum.Machine.Answering;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;
using rossum.Tools;

namespace rossum
{
    public static class Pipeline
    {
        public static void Run(IReader reader, ITokenizer tok, ISparseDistance dist, bool train, bool multipleAnswers, string questionFilePath, string encyclopediaFilePath, string outFolder)
        {
            string summary = reader.GetType().Name + "_" + tok.GetType().Name + "_" + dist.GetType().Name;
            Console.Write("\n" + summary);

            SparseMatcher robot = new SparseMatcher(dist, reader, tok);
            string[] answers = robot.SparseAnswer(questionFilePath, encyclopediaFilePath, train, multipleAnswers);

            if (train)
            {
                string[] actualAnswers = TextToData.ImportColumn(questionFilePath, 2);
                int good = 0;
                for (int i = 0; i < actualAnswers.Length; i++)
                    if (actualAnswers[i] == answers[i])
                        good++;
                Console.WriteLine("\nScore=" + good * 1f / answers.Length);
            }
            else
            {
                string[] ids = TextToData.ImportColumn(questionFilePath, 0);
                SubmissionWriter.Write(answers, ids, outFolder + summary + ".csv");
            }

            Console.WriteLine();
        }
    }
}
