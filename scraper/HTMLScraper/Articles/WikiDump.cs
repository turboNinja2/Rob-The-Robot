using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using rossum.Files;

namespace HTMLScraper.Articles
{
    public class WikiDump
    {
        private const int _BUFFER_SIZE_ = 100000;

        public string Run(string folder)
        {
            string outFilePath = folder + "Wiki.ency";
            string[] filesPaths = Directory.GetFiles(folder);

            File.WriteAllText(outFilePath, "");

            foreach (string filePath in filesPaths)
            {
                string rawArticle = "";
                foreach (string line in LinesEnumerator.YieldLines(filePath))
                {
                    rawArticle += line;
                    if (line.Contains("</page>"))
                    {
                        XmlDocument xmlArticle = new XmlDocument();
                        try
                        {
                            xmlArticle.LoadXml(rawArticle);
                            string innerText = xmlArticle.SelectSingleNode("page//text").InnerText;
                            innerText = CleanArticle(innerText);
                            if (innerText.StartsWith("#REDIRECT")) continue;
                            if (!Relevant(innerText)) continue;


                            File.AppendAllText(outFilePath, innerText + Environment.NewLine);
                        }
                        catch
                        {
                            rawArticle = "";
                            continue;
                        }
                        rawArticle = "";
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

            if (lowerCaseArticle.Contains("category:mecha")) return true;

            return false;

        }

        public static string CleanArticle(string article)
        {
            Regex htmlTag = new Regex("<.*?>");
            article = htmlTag.Replace(article, String.Empty);

            Regex roundBracket = new Regex("{{.*?}}");
            article = roundBracket.Replace(article, String.Empty);

            Regex section = new Regex("==.*?==");
            article = section.Replace(article, Environment.NewLine);

            article = article.Replace("[", "");
            article = article.Replace("]", "");
            article = article.Replace("'", "");
            article = article.Replace("=", " ");
            article = article.Replace("*", " ");

            Regex multipleSpaces = new Regex("[ ]+");
            article = multipleSpaces.Replace(article, " ");

            return article;
        }
    }
}
