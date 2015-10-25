using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace rossum.Answering
{
    public class RawQuestion
    {
        private string _rawText;

        private string _question;

        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;

        private string _answer;

        private bool _negativeQuestion = false;

        public string Answer
        {
            get { return _answer; }
        }

        public string Question
        {
            get { return _question; }
        }

        public bool Negated
        {
            get { return _negativeQuestion; }
        }

        public bool FillInTheGap
        {
            get { return _question.Contains(" __________"); }
        }

        public RawQuestion(string line, bool containsAnswer)
        {
            _rawText = line;
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

            if (_question.Contains("except"))
                _negativeQuestion = true;

        }

        public string[] GetCombinations()
        {

            if (_question.Contains(" __________")) // fill in the gap type of question
            {
                return new string[4]{_question.Replace("__________",_answerA),
                _question.Replace("__________",_answerB),
                _question.Replace("__________",_answerC),
                _question.Replace("__________",_answerD)};

            }
            else
            {
                return new string[4]{_question + " " + _answerA,
                _question + " " + _answerB,
                _question + " " + _answerC,
                _question + " " + _answerD};
            }
        }


        public string[] GetMarkovCombinations()
        {
            Regex whichIs = new Regex(@"[Ww]hich ([A-Za-z0-9\-]+) is ([A-Za-z0-9\-]+)");
            Match match = whichIs.Match(_question);

            if (_question.Contains(" __________")) // EASIEST type to detect : fill in the gap type of question
            {
                return new string[4]{_question.Replace("__________",_answerA),
                _question.Replace("__________",_answerB),
                _question.Replace("__________",_answerC),
                _question.Replace("__________",_answerD)};
            }
            else if (_question.Contains("Which of the following ")) // EASIEST type to detect : fill in the gap type of question
            {
                string[] res = new string[4]{_question.Replace("Which of the following",_answerA),
                _question.Replace("Which of the following",_answerB),
                _question.Replace("Which of the following",_answerC),
                _question.Replace("Which of the following",_answerD)};
                return res;
            }
            else
            {
                return new string[]{_question + " " + _answerA,
                    _question + " " + _answerB,
                    _question + " " + _answerC,
                    _question + " " + _answerD};
            }
        }

    }
}
