namespace rossum.Reading.Readers
{
    public class LowerCase : IReader
    {
        public string Read(string line)
        {
            line = line.ToLower();
            return line;
        }
    }
}
