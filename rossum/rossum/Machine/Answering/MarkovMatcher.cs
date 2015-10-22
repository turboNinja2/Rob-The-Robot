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
    public class MarkovMatcher
    {
        private SparseMarkovChain<string> _smc = new SparseMarkovChain<string>();
        private IReader _reader;
        private int _order;


        public MarkovMatcher(IReader reader, int order = 1)
        {
            _reader = reader;
            _order = order;
        }

        public void Learn(string inputFilePath)
        {
            foreach (string rawLine in LinesEnumerator.YieldLines(inputFilePath))
            {
                string readLine = _reader.Read(rawLine);
                string[] splitted = readLine.Split(' ').ToArray();
                if (splitted.Length < _order) continue;
                string[] stackedLine = Stack(splitted, _order);
                for (int i = _order; i < splitted.Length; i++)
                    _smc.AddTransition(splitted[i - _order], splitted[i]);
            }
        }

        private string[] Stack(string[] splittedLine, int order)
        {
            string[] res = new string[splittedLine.Length - order];
            for (int i = order; i < splittedLine.Length; i++)
            {
                string stamp = splittedLine[i];
                for (int k = 1; k < order; k++)
                    stamp = splittedLine[i - k] + " " + stamp;
                res[i - order] = stamp;
            }
            return res;
        }

        private string AnswerOneQuestion(RawQuestion mcq)
        {
            string question = mcq.Question;
            string[] proposals = mcq.GetMarkovCombinations();
            double[] likelihoods = new double[proposals.Length];
            for (int i = 0; i < likelihoods.Length; i++)
            {
                string readQuestion = _reader.Read(proposals[i]);
                string[] splittedQuestion = readQuestion.Split(' ').ToArray();
                string[] stackedQuestion = Stack(splittedQuestion, _order);
                likelihoods[i] = _smc.LengthNormalizedLogLikelihood(stackedQuestion);

            }
            double maxLikelihood = likelihoods.Max();
            int bestcandidate = Array.FindIndex(likelihoods, d => d == maxLikelihood);
            return IntToAnswers.ToAnswer(bestcandidate);
        }

        public string[] Answer(string questionnaireFilePath, bool train)
        {
            RawQuestion[] questions = QuestionnaireReader.Import(questionnaireFilePath, train);
            string[] results = new string[questions.Length];

            for (int k = 0; k < questions.Length; k++)
            {
                if ((k % DisplaySettings.PrintProgressEveryLine) == 0)
                    Console.Write('.');
                results[k] = AnswerOneQuestion(questions[k]);
            }
            return results;
        }
    }
}
