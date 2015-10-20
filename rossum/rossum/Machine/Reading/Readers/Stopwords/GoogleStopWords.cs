using System.Collections.Generic;

namespace rossum.Machine.Reading.Readers.Stopwords
{
    public class GoogleStopWords
    {
        string[] _data = "I a about an are as at be by com for from how in is it of on or that the this to was what when where who will with the www".Split(' ');
        HashSet<string> _res = new HashSet<string>();

        public GoogleStopWords()
        {
            foreach (string elt in _data)
                _res.Add(elt);
        }

        public HashSet<string> Get()
        {
            return _res;
        }

        public bool Contains(string elt)
        {
            return _res.Contains(elt);
        }
    }
}
