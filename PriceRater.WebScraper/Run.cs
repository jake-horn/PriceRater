using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper
{
    public class Run
    {
        private readonly IScraperController _scraperController;

        public Run(IScraperController scraperController)
        {
            _scraperController = scraperController;
        }

        public void ExecuteProgram()
        {
            do
            {
                string webAddress = Console.ReadLine();
                StartProgram(webAddress);
            }
            while (true);
        }

        private void StartProgram(string webAddress)
        {
            while(true)
            {
                IEnumerable<string> webAddressList = webAddress.Split(',').ToList();

                _scraperController.ScrapeMultipleProducts(webAddressList);

                // Addresses need to be validated before being executed here in future, both client side and server side
            }
        }
    }
}
