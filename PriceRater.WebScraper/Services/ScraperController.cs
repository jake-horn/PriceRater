using PriceRater.Common.Models;
using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper.Services
{
    public class ScraperController : IScraperController
    {
        private readonly IProductProviderService _productProviderService;

        public ScraperController(IProductProviderService productProviderService)
        {
            _productProviderService = productProviderService;
        }

        public IEnumerable<ProductDTO> ScrapeMultipleProducts(IEnumerable<string> webAddresses)
        {
            IList<ProductDTO> scrapedProducts = new List<ProductDTO>();

            foreach(var webAddress in webAddresses)
            {
                var product = ScrapeProduct(webAddress);
                scrapedProducts.Add(product);
            }

            return scrapedProducts;
        }

        public ProductDTO ScrapeProduct(string webAddress)
        {
            return _productProviderService.GetProductData(webAddress);
        }
    }
}
