using PriceRater.DataAccess.Interfaces;
using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper.Services
{
    public class ScraperController : IScraperController
    {
        private readonly IProductProviderService _productProviderService;
        private readonly IProductRepository _productRepository;

        public ScraperController(IProductProviderService productProviderService, IProductRepository productRepository)
        {
            _productProviderService = productProviderService;
            _productRepository = productRepository;
        }

        public void ScrapeSingleProduct(string webAddress)
        {
            var scrapedData = _productProviderService.GetProductData(webAddress);

            if (scrapedData is not null)
            {
                if (_productRepository.DoesProductExist(scrapedData))
                {
                    _productRepository.UpdateProduct(scrapedData);
                }
                else
                {
                    _productRepository.AddProduct(scrapedData);
                }
            }
        }

        public void ScrapeMultipleProduct(IEnumerable<string> webAddresses)
        {
            foreach(var webAddress in webAddresses)
            {
                ScrapeSingleProduct(webAddress);
            }
        }
    }
}
