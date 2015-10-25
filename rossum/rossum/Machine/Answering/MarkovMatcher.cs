using System;
using System.Linq;
using rossum.Answering;
using rossum.Files;
using rossum.Machine.Learning.Markov;
using rossum.Reading.Readers;
using rossum.Settings;
using rossum.Tools;

namespace rossum.Machine.Answering
{
    public class MarkovMatcher : IMatcher
    {
        private SparseMarkovChain<string> _smc = new SparseMarkovChain<string>();
        private IReader _reader;
        private int _order;
        private int _lag;

        public MarkovMatcher(IReader reader, int order, int lag)
        {
            _reader = reader;
            _order = order;
            _lag = lag;
        }

        public void Learn(string inputFilePath)
        {
            int linesRead = 0;
            foreach (string rawLine in LinesEnumerator.YieldLines(inputFilePath))
            {
                linesRead++;
                string readLine = _reader.Read(rawLine);
                string[] splitted = readLine.Split(' ').ToArray();
                if (splitted.Length < _order) continue;
                string[] stackedLine = Stack(splitted, _order, _lag);
                for (int i = 1; i < stackedLine.Length; i++)
                    if( stackedLine[i - 1]!= null && stackedLine[i] != null)
                        _smc.AddTransition(stackedLine[i - 1], stackedLine[i]);
                if ((linesRead % DisplaySettings.PrintProgressEveryLine) == 0)
                {
                    Console.Write('.');
                }
            }
        }

        public string[] Answer(string questionnaireFilePath, bool train, bool proba)
        {
            RawQuestion[] questions = QuestionnaireReader.Import(questionnaireFilePath, train);
            string[] results = new string[questions.Length];

            for (int k = 0; k < questions.Length; k++)
            {
                if ((k % DisplaySettings.PrintProgressEveryLine) == 0)
                    Console.Write('.');
                results[k] = AnswerOneQuestion(questions[k], proba);
            }
            return results;
        }

        private static string[] Stack(string[] splittedLine, int order, int lag)
        {
            string[] res = new string[splittedLine.Length - order];
            for (int i = order + lag; i < splittedLine.Length; i++)
            {
                string stamp = splittedLine[i];
                for (int k = 1; k < order; k++)
                    stamp = splittedLine[i - k - lag] + " " + stamp;
                res[i - order] = stamp;
            }
            return res;
        }

        private string AnswerOneQuestion(RawQuestion mcq, bool proba)
        {
            string question = mcq.Question;
            string[] proposals = mcq.GetMarkovCombinations();
            double[] likelihoods = new double[proposals.Length];
            for (int i = 0; i < likelihoods.Length; i++)
            {
                string readQuestion = _reader.Read(proposals[i]);
                string[] splittedQuestion = readQuestion.Split(' ').ToArray();
                string[] stackedQuestion = Stack(splittedQuestion, _order, _lag);
                likelihoods[i] = _smc.LengthNormalizedLogLikelihood(stackedQuestion);
            }

            double targetLikelihood = 0;
            if (mcq.Negated)
                targetLikelihood = likelihoods.Min();
            else
                targetLikelihood = likelihoods.Max();

            if (proba)
            {
                int[] candidates = likelihoods.Select((b, i) =>
                    b == targetLikelihood ? i : -1).Where(i => i != -1).ToArray();
                return String.Join(" ", candidates.Select(c =>
                    IntToAnswers.ToAnswer(c) + ":" + (1f / candidates.Length).ToString().Replace(',', '.')));
            }
            else
            {
                int bestcandidate = Array.FindIndex(likelihoods, d => d == targetLikelihood);
                return IntToAnswers.ToAnswer(bestcandidate);
            }

        }

    }
}
