using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rossum.Answering;
using rossum.Machine.Learning;
using rossum.Machine.Learning.SparseDistances;
using rossum.Reading;
using rossum.Reading.Readers;
using rossum.Settings;
using rossum.Tools;

namespace rossum.Machine.Answering
{
    public class Matcher
    {
        private ISparseDistance _distance;
        private IReader _reader;

        public Matcher(ISparseDistance distance, IReader reader)
        {
            _distance = distance;
            _reader = reader;
        }

        public string[] Answer(string questionnaireFilePath, string encyclopediaFilePath, bool train)
        {
            Console.Write("Import encyclopedia");
            Dictionary<string, double>[] encyclopedia = EncyclopediaReader.ImportSparse(encyclopediaFilePath, _reader);

            Console.Write("\n");

            Console.Write("Import questions");
            RawQuestion[] questions = QuestionnaireReader.Import(questionnaireFilePath, _reader, train);

            Console.Write("\nTrain KNN\n");
            SparseKNN<string> learner = new SparseKNN<string>(_distance.Value, 1, 200);
            learner.Train(encyclopedia);

            string[] results = new string[questions.Length];

            Console.Write("Started prediction");

            //for (int k = 0; k < questions.Length; k++)
            Parallel.For(0, questions.Length, k =>
            {
                
                if ((k % DisplaySettings.PrintProgressEveryLine) == 0)
                {
                    Console.Write('.');
                }

                RawQuestion question = questions[k];
                string[] proposals = question.GetCombinations();
                double[] distancesToEncyclopediaSpace = new double[proposals.Length];

                for (int i = 0; i < proposals.Length; i++)
                {
                    Dictionary<string, double> readQuestion = TextToData.Counts(proposals[i]);
                    distancesToEncyclopediaSpace[i] = learner.DistanceToClosestPoint(readQuestion);
                }

                int bestcandidate = Array.FindIndex(distancesToEncyclopediaSpace, d => d == distancesToEncyclopediaSpace.Min());

                results[k] = bestcandidate == 0 ? "A" : bestcandidate == 1 ? "B" : bestcandidate == 2 ? "C" : "D";
            });

            return results;
        }

    }
}
