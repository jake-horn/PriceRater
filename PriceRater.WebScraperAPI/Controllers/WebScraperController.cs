using Microsoft.AspNetCore.Mvc;

namespace PriceRater.WebScraperAPI.Controllers
{
    public class WebScraperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("scrapesingleproduct")]
        public IActionResult ScrapeSingleProduct()
        {
            return Ok();
        }
    }
}
