using HTMLScraper.Articles;

namespace HTMLScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            K12 k12 = new K12();
            k12.Run(@"C:\Users\JUJulien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\K12\");

            WikiDump wd = new WikiDump();
            wd.Run(@"C:\Users\JUJulien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\Wikidump\");
        }
    }
}
