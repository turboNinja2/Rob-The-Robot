﻿using System;
using System.IO;
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

            string summary = reader.GetType().Name + "_" + tok.GetType().Name + "_" + dist.GetType().Name + "_" + nbNeighbours.ToString() + "_" + encyclopediaName;
            Console.Write("\n" + summary);

            SparseMatcher robot = new SparseMatcher(dist, reader, tok);
            string[] answers = robot.SparseAnswer(nbNeighbours, questionFilePath, encyclopediaFilePath, train, proba);

            if (train)
            {
                string[] actualAnswers = TextToData.ImportColumn(questionFilePath, 2);
                int good = 0;
                for (int i = 0; i < actualAnswers.Length; i++)
                    if (actualAnswers[i] == answers[i])
                        good++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nScore=" + good * 1f / answers.Length);
                Console.ResetColor();
            }
            else
            {
                string[] ids = TextToData.ImportColumn(questionFilePath, 0);
                Submissions.Write(answers, ids, outFolder + summary + ".csv");
            }

            Console.WriteLine();
        }

        public static void MarkovRun(IReader reader, int order, bool train, string questionFilePath, string encyclopediaFilePath, string outFolder)
        {
            string encyclopediaName = Path.GetFileNameWithoutExtension(encyclopediaFilePath);

            string summary = reader.GetType().Name + "_" + order.ToString() + "_" + encyclopediaName;
            Console.Write("\n" + summary);

            MarkovMatcher mm = new MarkovMatcher(reader, order);
            mm.Learn(encyclopediaFilePath);
            string[] answers = mm.Answer(questionFilePath, train);

            if (train)
            {
                string[] actualAnswers = TextToData.ImportColumn(questionFilePath, 2);
                int good = 0;
                for (int i = 0; i < actualAnswers.Length; i++)
                    if (actualAnswers[i] == answers[i])
                        good++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nScore=" + good * 1f / answers.Length);
                Console.ResetColor();
            }
            else
            {
                string[] ids = TextToData.ImportColumn(questionFilePath, 0);
                Submissions.Write(answers, ids, outFolder + summary + ".csv");
            }

            Console.WriteLine();

        }


    }
}
