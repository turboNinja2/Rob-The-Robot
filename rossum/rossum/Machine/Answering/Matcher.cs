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

            Parallel.For(0, questions.Length, k =>
            {

                if ((k % DisplaySettings.PrintProgressEveryLine) == 0)
                {
                    Console.Write('.');
                }

                RawQuestion question = questions[k];
                string[] proposals = question.GetCombinations();
                double[] distancesToEncyclopedia = new double[proposals.Length];

                for (int i = 0; i < proposals.Length; i++)
                {
                    Dictionary<string, double> readQuestion = TextToData.Counts(proposals[i]);
                    distancesToEncyclopedia[i] = learner.DistanceToClosestPoint(readQuestion);
                }

                double minDistance = distancesToEncyclopedia.Min();

                int bestcandidate = Array.FindIndex(distancesToEncyclopedia, d => d == minDistance);
                int[] bestcandidates = distancesToEncyclopedia.Select((b, i) => b == minDistance ? i : -1).Where(i => i != -1).ToArray();

                results[k] = IntToAnswers.ToAnswer(bestcandidate);

                results[k] = String.Join(" ", bestcandidates.Select(c => IntToAnswers.ToAnswer(c)));


            });

            return results;
        }

    }
}
