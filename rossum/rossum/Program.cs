using System;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;
using rossum.Files;
using rossum.Machine.Answering;
using rossum.Machine.Reading.Readers.Stopwords;
using rossum.Machine.Reading.Stopwords;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(";w Started : " + DateTime.Now.ToString());

            string questionFilePath = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\All.ency",
                outFolder = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\submissions\Markov2\";

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
                for (int epochs = 1; epochs < 4; epochs++)
                {
                    for (int order = 0; order < 3; order++)
                    {
                        Pipeline.MarkovRun(new StemPunctuation(), new GoogleSW(), order, epochs, train, proba,
                            questionFilePath, encyclopediaFilePath, outFolder);

                        Pipeline.MarkovRun(new StemPunctuation(), new SQLSW(), order, epochs, train, proba,
                            questionFilePath, encyclopediaFilePath, outFolder);

                        Pipeline.MarkovRun(new StemPunctuation(), new ElargedSW(), order, epochs, train, proba,
                            questionFilePath, encyclopediaFilePath, outFolder);
                    }
                }
            }

            //Submissions.MergeMod(outFolder);

            int[] nbNeighboursArray = new int[] { 3, 5, 8, 10, 12, 15 };

            foreach (int nbNeighbours in nbNeighboursArray)
            {
                Pipeline.MetricRun(new StemPunctuation(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new StemPunctuation(), new TFIDF(encyclopediaFilePath, questionFilePath, new StemPunctuation(), train), new InformationDiffusion(),
                      nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            }

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}

