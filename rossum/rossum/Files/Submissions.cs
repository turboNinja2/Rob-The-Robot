using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using rossum.Tools;
using rossum.Machine.Learning;

namespace rossum.Files
{
    public class Submissions
    {
        public static void Write(string[] answers, string[] ids, string outFilePath)
        {
            File.WriteAllText(outFilePath, "id,correctAnswer" + Environment.NewLine);
            List<string> buffer = new List<string>();

            for (int i = 0; i < ids.Length; i++)
                buffer.Add(ids[i] + "," + answers[i]);

            File.AppendAllLines(outFilePath, buffer);

        }

        public static void MergeMod(string submissionFolder)
        {
            string[] filesPaths = Directory.GetFiles(submissionFolder);
            List<string[]> files = new List<string[]>();
            foreach (string filePath in filesPaths)
                files.Add(LinesEnumerator.YieldLines(filePath).ToArray());

            string outFilePath = submissionFolder + "\\merged.csv";

            File.WriteAllText(outFilePath, "id,correctAnswer" + Environment.NewLine);
            List<string> buffer = new List<string>();

            for (int i = 1; i < files[0].Length; i++)
            {
                List<Histogram<string>> answers = new List<Histogram<string>>();
                foreach (string[] file in files)
                {
                    answers.Add(TextToData.ParseString(file[i].Split(',')[1]));
                }

                string most = Histogram<string>.Merge(answers).MostLikelyElement();

                string id = files[0][i].Split(',')[0];
                buffer.Add(id + "," + most);
            }
            File.AppendAllLines(outFilePath, buffer);
        }
    }
}
