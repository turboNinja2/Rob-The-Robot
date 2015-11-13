using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using rossum.Files;
using System.Collections.Generic;

namespace HTMLScraper.Articles
{
    public class WikiDump
    {
        private const int _BUFFER_SIZE_ = 100000;

        public string Run(string folder)
        {
            string outFilePath = folder + "Wiki.ency";
            string[] filesPaths = Directory.GetFiles(folder);

            int linesRead = 0;

            List<string> buffer = new List<string>();

            File.WriteAllText(outFilePath, "");

            foreach (string filePath in filesPaths)
            {
                string rawArticle = "";
                foreach (string line in LinesEnumerator.YieldLines(filePath))
                {

                    rawArticle += line;

                    linesRead++;
                    if (line.Contains("</page>"))
                    {
                        XmlDocument xmlArticle = new XmlDocument();
                        try
                        {
                            xmlArticle.LoadXml(rawArticle);
                            string innerText = xmlArticle.SelectSingleNode("page//text").InnerText;

                            if (!Relevant(innerText)) continue;

                            innerText = RemoveAfterSubstring(innerText, "==See also==");

                            innerText = CleanArticle(innerText);
                            if (innerText.StartsWith("#REDIRECT")) continue;

                            buffer.Add(innerText);


                        }
                        catch
                        {
                            rawArticle = "";
                            continue;
                        }



                        rawArticle = "";
                    }

                    if (buffer.Count > 100)
                    {
                        File.AppendAllLines(outFilePath, buffer);
                        buffer.Clear();
                    }
                }
            }
            return outFilePath;
        }

        public static bool Relevant(string article)
        {
            string lowerCaseArticle = article.ToLower();

            if (lowerCaseArticle.Contains("category:elec")) return true;
            if (lowerCaseArticle.Contains("category:science")) return true;

            if (lowerCaseArticle.Contains("category:bio")) return true;
            if (lowerCaseArticle.Contains("category:gene")) return true;
            if (lowerCaseArticle.Contains("category:life")) return true;

            if (lowerCaseArticle.Contains("category:water")) return true;

            if (lowerCaseArticle.Contains("category:physic")) return true;
            if (lowerCaseArticle.Contains("category:chemis")) return true;
            if (lowerCaseArticle.Contains("category:gas")) return true;

            if (lowerCaseArticle.Contains("category:climat")) return true;

            if (lowerCaseArticle.Contains("category:astro")) return true;

            if (lowerCaseArticle.Contains("category:mecha")) return true;

            return false;

        }


        public static string RemoveAfterSubstring(string text, string substring)
        {
            int resultIndex = text.IndexOf(substring);
            if (resultIndex != -1)
            {
                text = text.Substring(0, resultIndex);
            }
            return text;
        }

        public static string CleanArticle(string article)
        {
            Regex htmlTag = new Regex("<.*?>");
            article = htmlTag.Replace(article, String.Empty);

            Regex roundBracket = new Regex("{{.*?}}");
            article = roundBracket.Replace(article, String.Empty);
            
            Regex section = new Regex("==.*?==");
            article = section.Replace(article, Environment.NewLine);

            Regex quotes = new Regex("ref&gt.*ref&gt");
            article = quotes.Replace(article, " ");

            article = article.Replace("[", "");
            article = article.Replace("]", "");
            article = article.Replace("'", "");
            article = article.Replace("=", " ");
            article = article.Replace("*", " ");
            article = article.Replace("|", " ");
            article = article.Replace(":", " ");
            article = article.Replace(";", " ");
            article = article.Replace("}", " ");
            article = article.Replace("{", " ");

            Regex multipleSpaces = new Regex("[ ]+");
            article = multipleSpaces.Replace(article, " ");

            return article;
        }
    }
}
