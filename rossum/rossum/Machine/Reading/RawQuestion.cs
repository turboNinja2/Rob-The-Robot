﻿using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace rossum.Answering
{
    /// <summary>
    /// Represent questions after being imported from a string. Contains the answer, 
    /// the candidate answers and the type of question (fill in the gap ? regular).
    /// </summary>
    public class RawQuestion
    {
        private static Regex _fillInTheGap = new Regex("[_]+");
        private static Regex _isNot = new Regex("[Ww]hich .* is not");

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
            get { return _fillInTheGap.IsMatch(_question); }
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

            if (_question.Contains("except") || _isNot.IsMatch(_question))
                _negativeQuestion = true;

        }

        public string[] GetCombinations()
        {

            if (FillInTheGap) // fill in the gap type of question
            {
                return new string[4]{_fillInTheGap.Replace(_question,_answerA),
                _fillInTheGap.Replace(_question,_answerB),
                _fillInTheGap.Replace(_question,_answerC),
                _fillInTheGap.Replace(_question,_answerD)};
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
            if (FillInTheGap) // EASIEST type to detect : fill in the gap type of question
            {
                return new string[]{_fillInTheGap.Replace(_question,_answerA),
                _fillInTheGap.Replace(_question,_answerB),
                _fillInTheGap.Replace(_question,_answerC),
                _fillInTheGap.Replace(_question,_answerD)};
            }
            else if (_question.Contains("Which of the following "))
            {
                string[] res = new string[]{_question.Replace("Which of the following",_answerA),
                _question.Replace("Which of the following",_answerB),
                _question.Replace("Which of the following",_answerC),
                _question.Replace("Which of the following",_answerD)};
                return res;
            }
            else if (_question.Contains("What is "))
            {
                string[] res = new string[]{
                    _question.Replace("What is",_answerA + " is"),
                    _question.Replace("What is",_answerB + " is"),
                    _question.Replace("What is",_answerC + " is"),
                    _question.Replace("What is",_answerD + " is"),
                    _question + " " + _answerA,
                    _question + " " + _answerB,
                    _question + " " + _answerC,
                    _question + " " + _answerD};
                return res;
            }
            else
            {
                string[] questionArray = _question.Split(' ').ToArray();
                string[] proposals = new string[4 * questionArray.Length];

                for (int i = 0; i < questionArray.Length; i++)
                {
                    questionArray = _question.Split(' ').ToArray();

                    questionArray[i] = _answerA;
                    proposals[4 * i] = string.Join(" ", questionArray);

                    questionArray[i] = _answerB;
                    proposals[4 * i + 1] = string.Join(" ", questionArray);

                    questionArray[i] = _answerC;
                    proposals[4 * i + 2] = string.Join(" ", questionArray);

                    questionArray[i] = _answerD;
                    proposals[4 * i + 3] = string.Join(" ", questionArray);
                }

                return new string[]{_question + " " + _answerA,
                    _question + " " + _answerB,
                    _question + " " + _answerC,
                    _question + " " + _answerD};
                //return proposals;
            }
        }

    }
}
