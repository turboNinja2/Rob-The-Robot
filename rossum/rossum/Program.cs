using System;
using rossum.Files;
using rossum.Machine.Answering;
using rossum.Machine.Learning.SparseDistances;
using rossum.Reading.Readers;
using rossum.Tools;
using rossum.Learning.SparseKernels;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\scraper\CK12.ency",
                outFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\_Answers.txt";
            bool train = true;


            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
                if (args[i] == "-train")
                {
                    questionFilePath = args[i + 1];
                    train = true;
                }
                if (args[i] == "-encyclopedia")
                    encyclopediaFilePath = args[i + 1];

                if (args[i] == "-test")
                {
                    questionFilePath = args[i + 1];
                    train = false;
                }

                if (args[i] == "-out")
                {
                    outFilePath = args[i + 1];
                }

            }

            IReader reader = new NaiveLowerCasePunctuation();

            //ISparseKernel myKernel = new Linear();
            //ISparseDistance myDist = new KernelDistance(myKernel);

            ISparseDistance myDist = new LevenshteinDistance();

            Matcher robot = new Matcher(myDist, reader);
            string[] answers = robot.Answer(questionFilePath, encyclopediaFilePath, train);

            if (train)
            {
                string[] actualAnswers = TextToData.ImportColumn(questionFilePath, 2);
                int good = 0;
                for (int i = 0; i < actualAnswers.Length; i++)
                    if (actualAnswers[i] == answers[i])
                        good++;
                Console.WriteLine("Score=" + good * 1f / answers.Length);
            }
            else
            {
                string[] ids = TextToData.ImportColumn(questionFilePath, 0);
                SubmissionWriter.Write(answers, ids, outFilePath);
            }
        }
    }
}
