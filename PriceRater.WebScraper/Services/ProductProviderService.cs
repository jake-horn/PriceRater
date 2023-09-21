using Microsoft.Extensions.Configuration;
using PriceRater.DataAccess.DTO;
using PriceRater.DataAccess.Interfaces;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Utilities.Exceptions;

namespace PriceRater.WebScraper.Services
{
    public class ProductProviderService : IProductProviderService
    {
        private readonly IRetailerConfigurationProvider _retailerConfigurationProvider;
        private readonly IProductScraperService _productDataProvider;
        private readonly IProductRepository _productRepository;

        public ProductProviderService(IRetailerConfigurationProvider retailerConfigurationProvider, 
                                      IProductScraperService productDataProvider, 
                                      IProductRepository productRepository)
        {
            _retailerConfigurationProvider = retailerConfigurationProvider;
            _productDataProvider = productDataProvider;
            _productRepository = productRepository;
        }

        public ProductDTO? GetProductData(string webAddress)
        {
            var retailerConfig = _retailerConfigurationProvider.GetRetailerConfiguration(webAddress)!;

            if (retailerConfig.Equals("Invalid"))
                throw new RetailerConfigurationException($"Invalid retailer for {webAddress}");

            try
            {
                var productData = _productDataProvider.ProvideProductData(retailerConfig, webAddress);

                return new ProductDTO()
                {
                    Title = productData.Title,
                    Price = productData.Price,
                    ClubcardPrice = productData.ClubcardPrice,
                    WebAddress = webAddress,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")!),
                    WebScrapingId = _productRepository.GetWebScrapingId(webAddress)
                };
            }
            catch (RetailerConfigurationException ex)
            {
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
                return null; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
                return null;
            }
        }
    }
}
