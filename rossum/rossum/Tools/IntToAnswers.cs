namespace rossum.Tools
{
    public static class IntToAnswers
    {
        public static string ToAnswer(int input)
        {
            return input == 0 ? "A" : input == 1 ? "B" : input == 2 ? "C" : "D";
        }
    }
}
