using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rossum.Reading.Readers;
using rossum.Reading;
using rossum.Answering;
using rossum.Learning.SparseKernels;
using rossum.Learning.SparseDistances;
using rossum.Machine.Answering;
using System.IO;
using rossum.Tools;
using rossum.Files;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\parsers\encyclopedia_small\_AllArticles.txt",
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

            IReader reader = new EnglishStemming();
            ISparseKernel linear = new Linear();
            ISparseDistance euclide = new KernelDistance(linear);

            Matcher robot = new Matcher(euclide, reader);
            string[] answers = robot.Answer(questionFilePath, encyclopediaFilePath, train);
            string[] ids = TextToData.ImportIds(questionFilePath);

            SubmissionWriter.Write(answers, ids, outFilePath);
        }
    }
}
