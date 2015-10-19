using System;
using rossum.Files;
using rossum.Machine.Answering;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;
using rossum.Tools;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;

            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\scraper\CK12.ency",
                outFolder = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\";
            bool train = true;
            bool multipleAnswers = false;


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
                    outFolder = args[i + 1];
            }

            IReader reader = new StemmingPunctuationStop();
            ITokenizer tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            ISparseDistance dist = new InformationDiffusion();

            Pipeline.Run(reader, tok, dist, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();

            Pipeline.Run(reader, tok, dist, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            Console.ReadKey();
        }
    }
}
