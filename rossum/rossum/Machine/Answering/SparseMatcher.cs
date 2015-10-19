using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rossum.Answering;
using rossum.Machine.Learning;
using rossum.Machine.Learning.SparseDistances;
using rossum.Machine.Reading.Tokenizers;
using rossum.Reading;
using rossum.Reading.Readers;
using rossum.Settings;
using rossum.Tools;

namespace rossum.Machine.Answering
{
    public class SparseMatcher
    {
        private ISparseDistance _distance;
        private IReader _reader;
        private ITokenizer _tokenizer;

        public SparseMatcher(ISparseDistance distance, IReader reader, ITokenizer tokenizer)
        {
            _distance = distance;
            _reader = reader;
            _tokenizer = tokenizer;
        }

        public string[] SparseAnswer(int nbNeighbours, string questionnaireFilePath, string encyclopediaFilePath, bool train, bool multipleAnswers)
        {
            Console.Write("\nImport encyclopedia");
            IDictionary<string, double>[] encyclopedia = EncyclopediaReader.ImportSparse(encyclopediaFilePath, _reader, _tokenizer);

            Console.Write("\nImport questions");
            RawQuestion[] questions = QuestionnaireReader.Import(questionnaireFilePath, _reader, train);

            Console.Write("\nTrain KNN");
            SparseKNN<string> learner = new SparseKNN<string>(_distance.Value, nbNeighbours, 2000);
            learner.Train(encyclopedia);

            string[] results = new string[questions.Length];

            Console.Write("\nStarted prediction");

            Parallel.For(0, questions.Length, k =>
            //for(int k = 0; k < questions.Length; k++)
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
                    IDictionary<string, double> readQuestion = _tokenizer.Tokenize(proposals[i]);
                    distancesToEncyclopedia[i] = learner.DistanceToClosestPoint(readQuestion);
                }

                double minDistance = distancesToEncyclopedia.Min();

                if (multipleAnswers)
                {
                    int[] bestcandidates = distancesToEncyclopedia.Select((b, i) => b == minDistance ? i : -1).Where(i => i != -1).ToArray();
                    results[k] = String.Join(" ", bestcandidates.Select(c => IntToAnswers.ToAnswer(c)));
                }
                else
                {
                    int bestcandidate = Array.FindIndex(distancesToEncyclopedia, d => d == minDistance);
                    results[k] = IntToAnswers.ToAnswer(bestcandidate);
                }

            });

            return results;
        }

    }
}
