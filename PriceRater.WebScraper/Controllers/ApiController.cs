using Microsoft.AspNetCore.Mvc;
using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper.Controllers
{
    [Route("scraper")]
    public class ApiController : ControllerBase
    {
        private readonly IScraperController _scraperController; 

        public ApiController(IScraperController scraperController)
        {
            _scraperController = scraperController;
        }

        [HttpPost("scrapeproduct")]
        public IActionResult ScrapeProduct([FromBody] string webAddress)
        {
            if (webAddress == null)
            {
                return BadRequest("null value passed");
            }

            var scrapeData = _scraperController.ScrapeProduct(webAddress);

            return Ok(scrapeData);
        }
    }
}
