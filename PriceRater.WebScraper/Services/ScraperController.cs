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

        public async Task<IEnumerable<ProductDTO>> ScrapeMultipleProducts(IEnumerable<string> webAddresses)
        {
            var semaphore = new SemaphoreSlim(5);
            var scrapeTasks = webAddresses.Select(webAddress => Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    return await _productProviderService.GetProductData(webAddress);
                }
                finally
                {
                    semaphore.Release();
                }
            }));

            var scrapedProducts = await Task.WhenAll(scrapeTasks);
            return scrapedProducts;
        }


        public async Task<ProductDTO> ScrapeProduct(string webAddress)
        {
            return await _productProviderService.GetProductData(webAddress); 
        }
    }
}
