using System;
using System.Collections.Generic;
using System.IO;

namespace rossum.Files
{
    public static class LinesEnumerator
    {
        /// <summary>
        /// Enumerates the lines of a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>The lines of a file, as a IEnumerable</returns>
        public static IEnumerable<string> YieldLines(string path, int maxLines = Int32.MaxValue)
        {
            string line;
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader sr = new StreamReader(fs))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
        }
    }
}