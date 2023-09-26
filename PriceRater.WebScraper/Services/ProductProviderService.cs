using Microsoft.Extensions.Configuration;
using PriceRater.Common.Models;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Models;
using PriceRater.WebScraper.Utilities.Exceptions;

namespace PriceRater.WebScraper.Services
{
    public class ProductProviderService : IProductProviderService
    {
        private readonly IRetailerConfigurationProvider _retailerConfigurationProvider;
        private readonly IProductScraperService _productDataProvider;

        public ProductProviderService(IRetailerConfigurationProvider retailerConfigurationProvider, IProductScraperService productDataProvider)
        {
            _retailerConfigurationProvider = retailerConfigurationProvider;
            _productDataProvider = productDataProvider;
        }

        public async Task<ProductDTO?> GetProductData(string webAddress)
        {
            var retailerConfig = await _retailerConfigurationProvider.GetRetailerConfiguration(webAddress)!;

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
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")!)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
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
