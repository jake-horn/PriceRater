using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PriceRater.Common.Models;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Utilities.Exceptions;

namespace PriceRater.WebScraper.Services
{
    public class ProductProviderService : IProductProviderService
    {
        private readonly IRetailerConfigurationProvider _retailerConfigurationProvider;
        private readonly IProductScraperService _productDataProvider;
        private readonly ILogger<IProductProviderService> _logger; 

        public ProductProviderService(IRetailerConfigurationProvider retailerConfigurationProvider, 
                                      IProductScraperService productDataProvider,
                                      ILogger<IProductProviderService> logger)
        {
            _retailerConfigurationProvider = retailerConfigurationProvider;
            _productDataProvider = productDataProvider;
            _logger = logger; 
        }

        public async Task<ProductDTO?> GetProductData(string webAddress)
        {
            var retailerConfig = await _retailerConfigurationProvider.GetRetailerConfiguration(webAddress)!;

            if (retailerConfig.Equals("Invalid"))
            {
                _logger.LogError($"Invalid retailer for web address {webAddress}.");
                throw new RetailerConfigurationException($"Invalid retailer for {webAddress}");
            }

            try
            {
                var productData = _productDataProvider.ProvideProductData(retailerConfig, webAddress);

                _logger.LogInformation($"Scraped data for {webAddress} successfully.");

                return new ProductDTO()
                {
                    Title = productData.Title,
                    Price = productData.Price,
                    ClubcardPrice = productData.ClubcardPrice,
                    WebAddress = webAddress,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")!)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to scrape {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");

                return new ProductDTO()
                {
                    Title = null,
                    Price = null,
                    ClubcardPrice = null,
                    WebAddress = webAddress,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")!)
                };
            }
        }
    }
}