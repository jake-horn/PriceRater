
using PriceRater.DataAccess.Interfaces;
using PriceRater.WebScraper.Services;

namespace PriceRater.WebScraper
{
    public class Run
    {
        private readonly IWebAddressProviderService _webAddressProviderService;
        private readonly IDataScraper _dataScraper;
        private readonly IProductRepository _productRepository;

        public Run(IWebAddressProviderService webAddressProviderService, IDataScraper dataScraper, IProductRepository productRepository)
        {
            _webAddressProviderService = webAddressProviderService;
            _dataScraper = dataScraper;
            _productRepository = productRepository;
        }

        public void StartProgram()
        {
            IDictionary<int, string> webAddresses = _webAddressProviderService.GetWebAddresses();

            foreach (var address in webAddresses)
            {
                var scrapedData = _dataScraper.ScrapeProductData(address.Key, address.Value);

                if (scrapedData is not null)
                {
                    _productRepository.AddProduct(scrapedData);
                }
            }
        }
    }
}
