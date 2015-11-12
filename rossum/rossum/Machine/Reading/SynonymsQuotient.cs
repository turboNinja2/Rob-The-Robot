using System.Collections.Generic;
using System.Linq;
using rossum.Files;

namespace rossum.Machine.Reading
{
    public class SynonymsQuotient
    {
        private Dictionary<string, string> _mapper = new Dictionary<string, string>();

        public SynonymsQuotient(string filePath, char sep = ',')
        {
            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                string[] elements = line.Split(sep).Where(c => !c.Contains(' ')).ToArray();
                for (int i = 1; i < elements.Length; i++)
                    if (!_mapper.ContainsKey(elements[i]))
                        _mapper.Add(elements[i], elements[0]);
            }
        }

        public string Map(string element)
        {
            if (_mapper.ContainsKey(element))
                return _mapper[element];
            else
                return element;
        }
    }
}
