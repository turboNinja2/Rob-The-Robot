using System;
using System.IO;
using rossum.Answering;
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
        public static void MetricRun(IReader reader, ITokenizer tok, ISparseDistance dist, int nbNeighbours, bool train, bool proba, string questionFilePath, string encyclopediaFilePath, string outFolder)
        {
            string encyclopediaName = Path.GetFileNameWithoutExtension(encyclopediaFilePath);

            string summary = "Metric_" + reader.GetType().Name + "_" + tok.GetType().Name + "_" + dist.GetType().Name + "_" + nbNeighbours.ToString() + "_" + encyclopediaName;
            Console.Write("\n" + summary);

            SparseMatcher robot = new SparseMatcher(dist, reader, tok, encyclopediaFilePath);
            string[] answers = robot.Answer(nbNeighbours, questionFilePath, train, proba);

            if (train)
            {
                EvaluateAndPrintScores(questionFilePath, answers);
            }
            else
            {
                string[] ids = TextToData.ImportColumn(questionFilePath, 0);
                Submissions.Write(answers, ids, outFolder + summary + ".csv");
            }

            Console.WriteLine();
        }

        public static void MarkovRun(IReader reader, int order, int lag, bool train, bool proba, string questionFilePath, string encyclopediaFilePath, string outFolder)
        {
            string encyclopediaName = Path.GetFileNameWithoutExtension(encyclopediaFilePath);

            string summary = "Markov_" + reader.GetType().Name + "_" + order.ToString() + "_" + lag.ToString() + "_" + encyclopediaName;
            Console.Write("\n" + summary);

            MarkovMatcher mm = new MarkovMatcher(reader, order, lag);
            mm.Learn(encyclopediaFilePath);
            string[] answers = mm.Answer(questionFilePath, train, proba);

            if (train)
            {
                EvaluateAndPrintScores(questionFilePath, answers);
            }
            else
            {
                string[] ids = TextToData.ImportColumn(questionFilePath, 0);
                Submissions.Write(answers, ids, outFolder + summary + ".csv");
            }

            Console.WriteLine();
        }

        private static double EvaluateErrors(string questionFilePath, string[] answers)
        {
            string[] actualAnswers = TextToData.ImportColumn(questionFilePath, 2);
            int good = 0;
            for (int i = 0; i < actualAnswers.Length; i++)
                if (actualAnswers[i] == answers[i])
                    good++;
            return good * 1f / answers.Length;
        }

        private static double EvaluateGapErrors(string questionFilePath, string[] answers)
        {
            RawQuestion[] questions = QuestionnaireReader.Import(questionFilePath, true);

            int good = 0,
                gaps = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                if (questions[i].FillInTheGap)
                {
                    gaps++;
                    if (questions[i].Answer == answers[i])
                        good++;
                }
            }
            return good * 1f / gaps;
        }

        private static void EvaluateAndPrintScores(string questionFilePath, string[] answers)
        {
            double score = EvaluateErrors(questionFilePath, answers);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nScore=" + score.ToString("0.##%"));
            Console.ResetColor();

            score = EvaluateGapErrors(questionFilePath, answers);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("\nGap  =" + score.ToString("0.##%"));
            Console.ResetColor();
        }
    }
}
