using System.Collections.Generic;

namespace rossum.Machine.Reading.Reworders
{
    public class GoogleSW : IReworder
    {
        string[] _data = "I a about an are as at be by com for from how in is it of on or that the this to was what when where who will with the www".Split(' ');
        HashSet<string> _res = new HashSet<string>();

        public GoogleSW()
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

        public string Map(string elt)
        {
            if (_res.Contains(elt))
                return "";
            else
                return elt;
        }
    }
}
