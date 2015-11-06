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
            Console.WriteLine(";w Started : " + DateTime.Now.ToString());

            string questionFilePath = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\CK12.ency",
                outFolder = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\submissions\";
            bool train = true;
            bool proba = false;
            bool markov = true;

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

                if (args[i] == "-prob")
                    proba = true;

                if (args[i] == "-markov")
                    markov = true;

            }
                    
            if (markov)
            {
                for (int order = 0; order < 3; order++)
                {
                    Pipeline.MarkovRun(new StemmingPunctuation(), order, train, proba,
                        questionFilePath, encyclopediaFilePath, outFolder);

                    Pipeline.MarkovRun(new StemmingPunctuationStop2(), order, train, proba,
                        questionFilePath, encyclopediaFilePath, outFolder);

                    Pipeline.MarkovRun(new StemmingPunctuationStop3(), order, train, proba,
                        questionFilePath, encyclopediaFilePath, outFolder);
                
                    Pipeline.MarkovRun(new StemmingPunctuationStop4(), order, train, proba,
                            questionFilePath, encyclopediaFilePath, outFolder);
                }

                Submissions.MergeMod(outFolder);
            }

            int[] nbNeighboursArray = new int[] { 3, 5, 8, 10, 12, 15 };

            foreach (int nbNeighbours in nbNeighboursArray)
            {
                /*
                Pipeline.MetricRun(new StemmingPunctuationStop(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop(), train), new CosineDistance(),
                     nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop3(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop3(), train), new CosineDistance(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);
                */

                Pipeline.MetricRun(new StemmingPunctuationStop(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop2(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop3(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop(), train), new InformationDiffusion(),
                      nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop2(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop(), train), new InformationDiffusion(),
                      nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop3(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop3(), train), new InformationDiffusion(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            }

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}

