using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static void MergeProba(string filepath1, string filepath2, string outFilePath)
        {
            File.WriteAllText(outFilePath, "id,correctAnswer" + Environment.NewLine);
            List<string> buffer = new List<string>();

            string[] file1 = LinesEnumerator.YieldLines(filepath1).ToArray();
            string[] file2 = LinesEnumerator.YieldLines(filepath2).ToArray();

            for (int i = 1; i < file1.Length; i++)
            {
                string[] line1 = file1[i].Split(',');
                string[] line2 = file2[i].Split(',');

                string id = line1[0];

                Dictionary<string, double> d1 = TextToData.ParseString(line1[1]);
                Dictionary<string, double> d2 = TextToData.ParseString(line2[1]);

                string[] keys = d1.Keys.ToArray();
                foreach (string key in keys)
                    d1[key] = d1[key] + d2[key];

                double highestProba = d1.Values.Max();
                string res = d1.Where(c => c.Value == highestProba).ElementAt(0).Key;

                buffer.Add(id + "," + res);

            }
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
                List<string> answers = new List<string>();
                foreach (string[] file in files)
                {
                    answers.Add(file[i].Split(',')[1]);
                }

                string most = answers.GroupBy(k => k).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();

                string id = files[0][i].Split(',')[0];
                buffer.Add(id + "," + most);
            }
            File.AppendAllLines(outFilePath, buffer);
        }
    }
}
