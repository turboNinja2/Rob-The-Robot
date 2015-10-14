namespace rossum.Answering
{
    public class Question
    {
        private string _question;

        private string _answerA;
        private string _answerB;
        private string _answerC;
        private string _answerD;

        private string _answer;

        public Question(string line, bool containsAnswer)
        {
            string[] splitted_line = line.Split('\t');

            if (containsAnswer)
            {
                _question = splitted_line[0];
                _answer = splitted_line[1];
                _answerA = splitted_line[2];
                _answerB = splitted_line[3];
                _answerC = splitted_line[4];
                _answerD = splitted_line[5];
            }
            else
            {
                _question = splitted_line[0];
                _answerA = splitted_line[1];
                _answerB = splitted_line[2];
                _answerC = splitted_line[3];
                _answerD = splitted_line[4];
            }
        }
    }
}
