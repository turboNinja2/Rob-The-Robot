using rossum.Machine.Reading;

namespace rossum.Reading.Readers
{
    public class LowerCasePunctuation : IReader
    {
        public string Read(string line)
        {
            line = StringHelper.RemovePunctuation(line);
            line = line.ToLower();
            return line;
        }
    }
}
