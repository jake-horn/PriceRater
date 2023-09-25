using PriceRater.DataAccess.DTO;
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

        public void ScrapeMultipleProducts(IEnumerable<string> webAddresses)
        {
            foreach(var webAddress in webAddresses)
            {
                ScrapeProduct(webAddress);
            }
        }

        public ProductDTO ScrapeProduct(string webAddress)
        {
            return _productProviderService.GetProductData(webAddress);

            /*if (scrapedData is not null)
            {
                if (_productRepository.DoesProductExist(scrapedData))
                {
                    _productRepository.UpdateProduct(scrapedData);
                }
                else
                {
                    _productRepository.AddProduct(scrapedData);
                }
            }*/
        }
    }
}
