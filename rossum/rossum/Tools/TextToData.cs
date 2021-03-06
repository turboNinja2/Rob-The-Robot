﻿using System;
using System.Collections.Generic;
using rossum.Files;
using rossum.Machine.Learning;

namespace rossum.Tools
{
    public static class TextToData
    {
        public static Histogram<string> ParseString(string line)
        {
            string[] splitted = line.Split(' ');
            Histogram<string> dic = new Histogram<string>();
            foreach (string elt in splitted)
            {
                if (elt.Contains(":"))
                    dic.UpdateKey(elt.Split(':')[0], Convert.ToDouble(elt.Split(':')[1].Replace('.', ',')));
                else
                    dic.UpdateKey(elt, 1);
            }
            return dic;
        }

        public static string[] ImportColumn(string filePath, int colIndex)
        {
            List<string> res = new List<string>();
            int linesRead = 0;
            foreach (string line in LinesEnumerator.YieldLines(filePath))
            {
                linesRead++;
                if (linesRead == 1) continue; //header
                res.Add(line.Split('\t')[colIndex]);
            }
            return res.ToArray();
        }
    }
}
