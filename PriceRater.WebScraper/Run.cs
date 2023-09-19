
using PriceRater.DataAccess.Interfaces;
using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper
{
    public class Run
    {
        private readonly IWebAddressProviderService _webAddressProviderService;
        private readonly IProductProviderService _dataScraper;
        private readonly IProductRepository _productRepository;

        public Run(IWebAddressProviderService webAddressProviderService, IProductProviderService dataScraper, IProductRepository productRepository)
        {
            _webAddressProviderService = webAddressProviderService;
            _dataScraper = dataScraper;
            _productRepository = productRepository;
        }

        public void StartProgram()
        {
            //IDictionary<int, string> webAddresses = _webAddressProviderService.GetWebAddresses();
            IDictionary<int, string> webAddresses = new Dictionary<int, string>() 
            {
                { 67, "https://www.tesco.com/groceries/en-GB/products/313834076" } 
            };

            foreach (var address in webAddresses)
            {
                var scrapedData = _dataScraper.GetProductData(address.Key, address.Value);

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
        }
    }
}
