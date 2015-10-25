namespace rossum.Machine.Reading
{
    public static class StringHelper
    {
        public static string RemovePunctuation(string line)
        {
            line = line.Replace(".", "");
            line = line.Replace(",", "");

            line = line.Replace("\"", "");
            line = line.Replace("?", "");

            line = line.Replace("(", "");
            line = line.Replace(")", "");

            line = line.Replace("-", " ");

            return line;
        }
    }
}
