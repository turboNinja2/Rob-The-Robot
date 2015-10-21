using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static string RemovePunctuation2(string line)
        {
            line = RemovePunctuation(line);

            line = line.Replace(";", "");
            line = line.Replace(":", "");


            return line;
        }
    }
}
