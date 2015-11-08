using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using rossum.Files;

namespace HTMLScraper.Articles
{
    public class K12
    {
        public string Run(string folder)
        {
            string[] filesPaths = Directory.GetFiles(folder);

            Regex htmlTag = new Regex("<.*?>");
            Regex htmlTag2 = new Regex("&.*?;");
            Regex answer = new Regex("[A-D]");

            List<string> buffer = new List<string>();
            foreach (string filePath in filesPaths)
            {
                foreach (string line in LinesEnumerator.YieldLines(filePath))
                {
                    string modifiedLine = htmlTag.Replace(line, "");
                    modifiedLine = htmlTag2.Replace(modifiedLine, "");

                    if (line.Contains("alert('Incorrect')")) continue;
                    if (line.Contains("name=\"answer")) continue;
                    if (line.Contains("alert('Correct!')"))
                    {
                        buffer[buffer.Count - 1] = buffer[buffer.Count - 1] + answer.Replace(modifiedLine, "").Replace(")", " ");
                        continue;
                    }

                    if (modifiedLine.Length > 1)
                        buffer.Add(modifiedLine);
                }
            }
            File.WriteAllLines(folder + "\\K12.ency", buffer.ToArray());
            return folder + "\\K12.ency";
        }
    }
}
