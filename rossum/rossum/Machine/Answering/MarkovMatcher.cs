using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rossum.Machine.Learning.Markov;
using rossum.Machine.Learning.SparseDistances;
using rossum.Reading.Readers;
using rossum.Machine.Reading.Tokenizers;
using rossum.Files;
using rossum.Answering;

namespace rossum.Machine.Answering
{
    public class MarkovMatcher
    {
        private SparseMarkovChain<string> _smc = new SparseMarkovChain<string>();
        private IReader _reader;
        private int _lag;


        public MarkovMatcher(IReader reader, int lag = 1)
        {
            _reader = reader;
            _lag = lag;
        }

        public void Learn(string inputFilePath)
        {
            foreach (string rawLine in LinesEnumerator.YieldLines(inputFilePath))
            {
                string readLine = _reader.Read(rawLine);
                string[] splitted = readLine.Split(' ').ToArray();
                for (int i = _lag; i < splitted.Length; i++)
                    _smc.AddTransition(splitted[i - _lag], splitted[i]);
            }
        }

        public void AnswerOneQuestion(RawQuestion mcq)
        {
            string question = mcq.Question;
            
            if(question.Contains("__________")); // fill in the gap
            {

            }
        }

    }
}
