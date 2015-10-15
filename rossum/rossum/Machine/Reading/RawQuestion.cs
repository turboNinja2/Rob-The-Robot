using System.Collections.Generic;

namespace rossum.Answering
{
    public class RawQuestion
    {
        private string _question;

        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;

        private string _answer;

        public RawQuestion(string line, bool containsAnswer)
        {
            string[] splitted_line = line.Split('\t');

            if (containsAnswer)
            {
                _question = splitted_line[1];
                _answer = splitted_line[2];
                _answerA = splitted_line[3];
                _answerB = splitted_line[4];
                _answerC = splitted_line[5];
                _answerD = splitted_line[6];
            }
            else
            {
                _question = splitted_line[1];
                _answerA = splitted_line[2];
                _answerB = splitted_line[3];
                _answerC = splitted_line[4];
                _answerD = splitted_line[5];
            }
        }

        public string[] GetCombinations()
        {
            return new string[4]{_question + " " + _answerA,
                _question + " " + _answerB,
                _question + " " + _answerC,
                _question + " " + _answerD};
        }
    }
}
