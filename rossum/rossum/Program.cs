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
            Console.WriteLine(";-mmm Started : " + DateTime.Now.ToString());

            string questionFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\scraper\Wikipedia.ency",
                outFolder = @"C:\Users\Windows\Desktop\R\Rob-The-Robot\submissions\";
            bool train = true;
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

            int order = 0;

            Pipeline.MarkovRun(new LowerCasePunctuation(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            Pipeline.MarkovRun(new StemmingPunctuation(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            Pipeline.MarkovRun(new StemmingPunctuationStop2(), order, train, questionFilePath, encyclopediaFilePath, outFolder);

            order = 1;
            Pipeline.MarkovRun(new LowerCasePunctuation(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            Pipeline.MarkovRun(new StemmingPunctuation(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            Pipeline.MarkovRun(new StemmingPunctuationStop2(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            order = 2;
            Pipeline.MarkovRun(new LowerCasePunctuation(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            Pipeline.MarkovRun(new StemmingPunctuation(), order, train, questionFilePath, encyclopediaFilePath, outFolder);
            Pipeline.MarkovRun(new StemmingPunctuationStop2(), order, train, questionFilePath, encyclopediaFilePath, outFolder);


            int[] nbNeighboursArray = new int[5] { 1, 3, 5, 10, 15 };

            foreach (int nbNeighbours in nbNeighboursArray)
            {
                Pipeline.MetricRun(new StemmingPunctuationStop(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop(), train), new InformationDiffusion(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop2(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop2(), train), new InformationDiffusion(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);


                Pipeline.MetricRun(new StemmingPunctuationStop(), new BigramTFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop(), train), new InformationDiffusion(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop2(), new BigramTFIDF(encyclopediaFilePath, questionFilePath, new StemmingPunctuationStop2(), train), new InformationDiffusion(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);


                Pipeline.MetricRun(new LowerCasePunctuation(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemmingPunctuationStop2(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);
            }

        }
    }
}
