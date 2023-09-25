using Microsoft.AspNetCore.Mvc;

namespace PriceRater.API.Controllers
{
    public class WebScraperController : Controller
    {

        [HttpPost("scrapesingleproduct")]
        public IActionResult ScrapeSingleProduct(string webAddress)
        {
            return Ok();
        }

        [HttpPost("scrapemultipleproducts")]
        public IActionResult ScrapeMultipleProducts(List<string> webAddresses)
        {
            return Ok();
        }
    }
}
