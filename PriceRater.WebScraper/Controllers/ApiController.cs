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
        public async Task<IActionResult> ScrapeProduct([FromBody] string webAddress)
        {
            if (webAddress == null)
            {
                return BadRequest("null value passed");
            }

            var scrapeData = await _scraperController.ScrapeProduct(webAddress);

            return Ok(scrapeData);
        }

        [HttpPost("scrapemultipleproducts")]
        public IActionResult ScrapeMultipleProducts([FromBody] string webAddresses) 
        {
            if (webAddresses == null)
            {
                return BadRequest("null value passed");
            }

            IEnumerable<string> webAddressList = webAddresses.Split(',').ToList();

            var scrapedAddresses = _scraperController.ScrapeMultipleProducts(webAddressList);

            return Ok(scrapedAddresses);
        }
    }
}
