using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rossum.Machine.Learning.Markov;
using rossum.Machine.Learning.SparseDistances;
using rossum.Reading.Readers;
using rossum.Machine.Reading.Tokenizers;
using rossum.Files;

namespace rossum.Machine.Answering
{
    public class MarkovMatcher
    {
        private SparseMarkovChain<string> _smc = new SparseMarkovChain<string>();
        private IReader _reader;
        private ITokenizer _tokenizer;

        /// <summary>
        /// Instantiates a new MarkovMatcher. 
        /// Note that the tokenizer must preserve the order of the input.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="reader"></param>
        /// <param name="tokenizer"></param>
        public MarkovMatcher(IReader reader)
        {
            _reader = reader;
        }

        public void Learn(string inputFilePath)
        {
            foreach(string rawLine in LinesEnumerator.YieldLines(inputFilePath))
            {
                string readLine = _reader.Read(rawLine);
                string[] splitted = readLine.Split(' ').ToArray();
                for (int i = 1; i < splitted.Length; i++)
                    _smc.AddTransition(splitted[i - 1], splitted[i]);
            }
        }

    }
}
