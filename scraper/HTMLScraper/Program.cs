using HTMLScraper.Articles;

namespace HTMLScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            WikiDump wd = new WikiDump();
            wd.Run(@"C:\Users\Julien\Desktop\KAGGLE\Competitions\Rob-The-Robot\scraper\WikiDump\");
        }
    }
}
