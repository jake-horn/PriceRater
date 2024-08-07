﻿using Microsoft.Extensions.Configuration;
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
        private const int MAX_ATTEMPTS = 2; 

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
            var retailerConfig = await GetRetailerConfigAsync(webAddress);

            for (int attempts = 1; attempts <= MAX_ATTEMPTS; attempts++)
            {
                try
                {
                    var productData = _productDataProvider.ProvideProductData(retailerConfig, webAddress);

                    _logger.LogInformation("Scraped data for {webAddress} successfully.", webAddress);

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
                    _logger.LogError("Failed to scrape {WebAddress}, error: {ErrorName} : {Message}", webAddress, ex.GetType().FullName, ex.Message);
                }

                if (attempts == MAX_ATTEMPTS)
                {
                    throw new ScraperFailureException($"Failed to scrape {webAddress} after MAX_ATTEMPTS.");
                }
            }

            throw new InvalidOperationException("Unexpected error in GetProductData outside of loop.");
        }

        private async Task<IConfiguration> GetRetailerConfigAsync(string webAddress)
        {
            var retailerConfig = await _retailerConfigurationProvider.GetRetailerConfiguration(webAddress);

            if (retailerConfig!.Equals("Invalid") || retailerConfig is null)
            {
                _logger.LogError("Invalid retailer for web address {webAddress}.", webAddress);
                throw new RetailerConfigurationException($"Invalid retailer for {webAddress}");
            }

            return retailerConfig;
        }
    }
}