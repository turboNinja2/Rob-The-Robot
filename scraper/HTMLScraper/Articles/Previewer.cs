using System.Collections.Generic;
using System.IO;
using rossum.Files;

namespace HTMLScraper.Articles
{
    public class Previewer
    {
        public void Run(string folder)
        {
            string[] filesPaths = Directory.GetFiles(folder);
            List<string> buffer = new List<string>();

            int linesRead = 0;

            foreach (string line in LinesEnumerator.YieldLines(filesPaths[0]))
            {
                linesRead++;
                if (line.Length > 1)
                    buffer.Add(line);
                if (linesRead > 10000)
                    break;
            }

            File.WriteAllLines(folder + "\\preview.ency", buffer.ToArray());


        }
    }
}
