using System;
using rossum.Files;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Reworders;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading.Readers;

namespace rossum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(";tl Started : " + DateTime.Now.ToString());

            string questionFilePath = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\data\training_set.tsv",
                encyclopediaFilePath = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\All.ency",
                outFolder = @"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\submissions\Markov2\",
                synonymsFilePath = "";

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


                if (args[i] == "-synonyms")
                    synonymsFilePath = args[i+1];
            }

            if (markov)
            {
                for (int epochs = 1; epochs < 5; epochs++)
                {
                    for (int order = 0; order < 3; order += 2)
                    {
                        Pipeline.MarkovRun(new StemPunctuationY(), new ElargedSW(), order, epochs, false, train, proba,
                            questionFilePath, encyclopediaFilePath, outFolder);

                        Pipeline.MarkovRun(new StemPunctuationY(), new ElargedSW(), order, epochs, true, train, proba,
                            questionFilePath, encyclopediaFilePath, outFolder);

                    }
                }
            }

            //Submissions.MergeMod(outFolder);

            int[] nbNeighboursArray = new int[] { 3, 5, 8, 10, 12, 15 };

            foreach (int nbNeighbours in nbNeighboursArray)
            {
                Pipeline.MetricRun(new GoogleSW(), new StemPunctuation(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new SQLSW(), new StemPunctuation(), new Counts(), new NormalizedJaccard(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new GoogleSW(), new StemPunctuation(), new Counts(), new Tanimoto(),
                    nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new GoogleSW(), new StemPunctuation(), new TFIDF(encyclopediaFilePath, questionFilePath, new GoogleSW(), new StemPunctuation(), train), new InformationDiffusion(),
                      nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new SQLSW(), new StemPunctuation(), new TFIDF(encyclopediaFilePath, questionFilePath, new SQLSW(), new StemPunctuation(), train), new InformationDiffusion(),
                      nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

                Pipeline.MetricRun(new ElargedSW(), new StemPunctuationY(), new TFIDF(encyclopediaFilePath, questionFilePath, new ElargedSW(), new StemPunctuationY(), train), new InformationDiffusion(),
                      nbNeighbours, train, proba, questionFilePath, encyclopediaFilePath, outFolder);

            }

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}

