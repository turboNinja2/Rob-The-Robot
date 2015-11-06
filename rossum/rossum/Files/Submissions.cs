using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using rossum.Machine.Learning;
using rossum.Tools;

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

            string outFilePath = submissionFolder + "\\merged.csv",
                outFileProbaPath = submissionFolder + "\\probas.csv";

            File.WriteAllText(outFilePath, "id,correctAnswer" + Environment.NewLine);
            File.WriteAllText(outFileProbaPath, "id,correctAnswer" + Environment.NewLine);

            List<string> buffer = new List<string>(),
                bufferProba = new List<string>();

            for (int i = 1; i < files[0].Length; i++)
            {
                List<Histogram<string>> answers = new List<Histogram<string>>();
                foreach (string[] file in files)
                {
                    string currentDic = file[i].Split(',')[1];
                    if (currentDic.Length > 0)
                        answers.Add(TextToData.ParseString(currentDic));
                }

                Histogram<string> merged = Histogram<string>.Merge(answers);
                string most = merged.MostLikelyElement();

                string id = files[0][i].Split(',')[0];
                buffer.Add(id + "," + most);
                bufferProba.Add(id + ',' + merged.ToString());
            }
            File.AppendAllLines(outFilePath, buffer);
            File.AppendAllLines(outFileProbaPath, bufferProba);
        }
    }
}
