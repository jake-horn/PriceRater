using Microsoft.AspNetCore.Mvc;
using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraperAPI.Controllers
{
    public class WebScraperController : Controller
    {
        private readonly IScraperController _scraperController;

        public WebScraperController(IScraperController scraperController)
        {
            _scraperController = scraperController;
        }

        [HttpPost("scrapesingleproduct")]
        public IActionResult ScrapeSingleProduct(string webAddress)
        {
            _scraperController.ScrapeSingleProduct(webAddress);
            return Ok();
        }
    }
}
