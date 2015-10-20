using System;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;
using rossum.Files;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;

            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\validation_set.tsv",
                encyclopediaFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\scraper\All.ency",
                outFolder = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\submissions\";
            bool train = false;
            bool proba = false;


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

                if (args[i] == "-merge")
                {
                    string submissionFolder = args[i + 1];
                    Submissions.MergeMod(submissionFolder);
                    return;
                }
            }

            /*
            string submissionFolder = outFolder + "\\2\\";
            Submissions.MergeMod(submissionFolder);
            */

            IReader reader = new StemmingPunctuationStop();
            ITokenizer tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            ISparseDistance dist = new InformationDiffusion();
            int nbNeighbours = 1;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();
            nbNeighbours = 5;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

        }
    }
}
