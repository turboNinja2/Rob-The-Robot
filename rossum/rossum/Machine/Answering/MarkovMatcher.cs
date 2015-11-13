using System;
using System.Linq;
using System.Text.RegularExpressions;
using rossum.Answering;
using rossum.Files;
using rossum.Machine.Learning.Markov;
using rossum.Reading.Readers;
using rossum.Settings;
using rossum.Tools;
using rossum.Machine.Reading.Reworders;

namespace rossum.Machine.Answering
{
    public class MarkovMatcher : IMatcher
    {
        #region Private attributes
        private SparseMarkovChain<string> _smc = new SparseMarkovChain<string>();
        private IReader _reader;
        private IReworder _reworder;
        private int _order;
        private int _epochs;
        private Random _rnd = new Random(1);
        private bool _randomizedRestack;
        #endregion

        public MarkovMatcher(IReader reader, IReworder reworder, int order, int epochs, bool randomizedRestack)
        {
            _reader = reader;
            _order = order;
            _epochs = epochs;
            _reworder = reworder;
            _randomizedRestack = randomizedRestack;
        }

        public void Learn(string inputFilePath)
        {
            int linesRead = 0;

            foreach (string rawLine in LinesEnumerator.YieldLines(inputFilePath))
            {
                linesRead++;
                string mappedLine = ReworderHelper.Map(rawLine, _reworder);
                string readLine = _reader.Read(mappedLine);

                Regex multipleSpaces = new Regex("[ ]+");
                readLine = multipleSpaces.Replace(readLine, " ");

                string[] splitted = readLine.Split(' ').ToArray();
                if (splitted.Length < _order) continue;

                string[] stackedLine = Stack(splitted, _order);

                if (_randomizedRestack)
                    stackedLine = SmartIndexes.Merge<string>(stackedLine, stackedLine.Where(c => _rnd.NextDouble() > 0.5).ToArray());

                for (int epoch = 0; epoch < _epochs; epoch++)
                    for (int i = 1; i < stackedLine.Length; i++)
                        if (stackedLine[i - 1] != null && stackedLine[i] != null)
                            _smc.AddTransition(stackedLine[i - 1], stackedLine[i]);

                if ((linesRead % DisplaySettings.PrintProgressEveryLine) == 0)
                    Console.Write('.');
            }

            Console.Write("Words read: " + _smc.Count);

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

        private string AnswerOneQuestion(RawQuestion mcq, bool proba)
        {
            string question = mcq.Question;
            string[] proposals = mcq.GetMarkovCombinations();
            double[] likelihoods = new double[proposals.Length];
            for (int i = 0; i < likelihoods.Length; i++)
            {
                string mappedLine = ReworderHelper.Map(proposals[i], _reworder);
                string readQuestion = _reader.Read(mappedLine);

                // should not be there, simple precaution
                Regex multipleSpaces = new Regex("[ ]+");
                readQuestion = multipleSpaces.Replace(readQuestion, " ");

                string[] splittedQuestion = readQuestion.Split(' ').ToArray();
                string[] stackedQuestion = Stack(splittedQuestion, _order);
                likelihoods[i] = _smc.LengthNormalizedLogLikelihood(stackedQuestion);
            }

            double targetLikelihood = 0;
            if (mcq.Negated)
                targetLikelihood = likelihoods.Max();
            else
                targetLikelihood = likelihoods.Max();

            if (proba)
            {
                int[] candidates = likelihoods.Select((b, i) =>
                    b == targetLikelihood ? i : -1).Where(i => i != -1).ToArray();
                return String.Join(" ", candidates.Select(c =>
                    IntToAnswers.ToAnswer(c % 4) + ":" + (1f / candidates.Length).ToString().Replace(',', '.')));
            }
            else
            {
                int bestcandidate = Array.FindIndex(likelihoods, d => d == targetLikelihood) % 4;
                return IntToAnswers.ToAnswer(bestcandidate);
            }
        }

        private static string[] Stack(string[] splittedLine, int order)
        {
            string[] intm = new string[splittedLine.Length - order];

            for (int i = order; i < splittedLine.Length; i++)
            {
                string stamp = splittedLine[i];
                for (int k = 1; k < order; k++)
                    stamp = splittedLine[i - k] + " " + stamp;
                intm[i - order] = stamp;
            }
            return intm;
        }

    }
}
