using System;
using System.Collections.Generic;
using System.IO;

namespace rossum.Files
{
    public class SubmissionWriter
    {
        public static void Write(string[] answers, string[] ids, string outFilePath)
        {
            File.WriteAllText(outFilePath, "id,correctAnswer" + Environment.NewLine);
            List<string> buffer = new List<string>(); 
            
            for (int i = 0; i < ids.Length; i++)
                buffer.Add(ids[i] + "," + answers[i]);

            File.AppendAllLines(outFilePath, buffer);

        }
    }
}
