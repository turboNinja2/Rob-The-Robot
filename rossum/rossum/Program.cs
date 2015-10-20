using System;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;

            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\scraper\All.ency",
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
            int nbNeighbours = 1;

            
            Pipeline.Run(reader, tok, dist,nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new CosineDistance();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new OrderedCounts();
            dist = new SortedLevenshtein();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);


            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new InformationDiffusion();
            nbNeighbours = 2;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new CosineDistance();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new OrderedCounts();
            dist = new SortedLevenshtein();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);


            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new InformationDiffusion();
            nbNeighbours = 3;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new CosineDistance();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new OrderedCounts();
            dist = new SortedLevenshtein();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new InformationDiffusion();
            nbNeighbours = 6;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new CosineDistance();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new OrderedCounts();
            dist = new SortedLevenshtein();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new InformationDiffusion();
            nbNeighbours = 10;

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new TFIDF(encyclopediaFilePath, questionFilePath, reader);
            dist = new CosineDistance();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new LowerCasePunctuation();
            tok = new Counts();
            dist = new NormalizedJaccard();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);

            reader = new StemmingPunctuationStop();
            tok = new OrderedCounts();
            dist = new SortedLevenshtein();

            Pipeline.Run(reader, tok, dist, nbNeighbours, train, multipleAnswers, questionFilePath, encyclopediaFilePath, outFolder);
            

            Console.ReadKey();
        }
    }
}
