using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace rossum.Machine.Reading.Reworders
{
    public static class ReworderHelper
    {
        public static string Map(string input, IReworder reworder)
        {
            string res = String.Join(" ", input.Split(' ').Select(c => reworder.Map(c.ToLower())));

            Regex multipleSpaces = new Regex("[ ]+");
            res = multipleSpaces.Replace(res, " ");
            return res;

        }
    }
}
