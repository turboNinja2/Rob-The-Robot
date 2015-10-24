using System;
using System.Collections.Generic;
using System.Linq;
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

        public string[] SparseAnswer(int nbNeighbours, string questionnaireFilePath, string encyclopediaFilePath, bool train, bool proba)
        {
            //Console.Write("\nImport Encyclopedia");
            IDictionary<string, double>[] encyclopedia = EncyclopediaReader.ImportSparse(encyclopediaFilePath, _reader, _tokenizer);

            //Console.Write("\nImport Questions");
            RawQuestion[] questions = QuestionnaireReader.Import(questionnaireFilePath, train);

            //Console.Write("\nTrain KNN");
            SparseKNN<string> learner = new SparseKNN<string>(_distance.Value, nbNeighbours, 5000);
            learner.Train(encyclopedia);

            string[] results = new string[questions.Length];

            Console.Write("\nStarted prediction");

            for (int k = 0; k < questions.Length; k++)
            {
                if ((k % DisplaySettings.PrintProgressEveryLine) == 0)
                    Console.Write('.');

                RawQuestion question = questions[k];
                string[] proposals = question.GetCombinations();
                double[] distancesToEncyclopedia = new double[proposals.Length];

                for (int i = 0; i < proposals.Length; i++)
                {
                    IDictionary<string, double> readQuestion = _tokenizer.Tokenize(_reader.Read(proposals[i]));
                    distancesToEncyclopedia[i] = learner.DistanceToClosestPoint(readQuestion);
                }

                double targetDistance = distancesToEncyclopedia.Min();

                if (question.Negated)
                    targetDistance = distancesToEncyclopedia.Max();

                if (proba)
                {
                    int[] candidates = distancesToEncyclopedia.Select((b, i) => 
                        b == targetDistance ? i : -1).Where(i => i != -1).ToArray();
                    results[k] = String.Join(" ", candidates.Select(c => 
                        IntToAnswers.ToAnswer(c) + ":" + (1f / candidates.Length).ToString().Replace(',','.')));
                }
                else
                {
                    int bestcandidate = Array.FindIndex(distancesToEncyclopedia, d => d == targetDistance);
                    results[k] = IntToAnswers.ToAnswer(bestcandidate);
                }
            }

            return results;
        }

    }
}
