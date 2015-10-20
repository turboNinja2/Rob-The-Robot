using System;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;
using rossum.Files;
using rossum.Machine.Answering;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;

            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\validation_set.tsv",
                encyclopediaFilePath = @"C:\Users\JUJulien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\CK12.ency",
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

            IReader reader = new StemmingPunctuationStop();

            MarkovMatcher mm = new MarkovMatcher(reader);
            mm.Learn(encyclopediaFilePath);

            ITokenizer tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            ISparseDistance dist = new InformationDiffusion();
            int nbNeighbours = 1;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new InformationDiffusion();
            nbNeighbours = 5;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();
            nbNeighbours = 1;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();
            nbNeighbours = 5;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

        }
    }
}
