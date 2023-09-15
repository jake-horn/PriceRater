﻿using Microsoft.Extensions.Configuration;
using PriceRater.DataAccess.DTO;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Utilities.Exceptions;

namespace PriceRater.WebScraper.Services
{
    public class DataScraper : IDataScraper
    {
        private readonly IRetailerConfigurationProvider _retailerConfigurationProvider;
        private readonly IProductDataProvider _productDataProvider;

        public DataScraper(IRetailerConfigurationProvider retailerConfigurationProvider, IProductDataProvider productDataProvider)
        {
            _retailerConfigurationProvider = retailerConfigurationProvider;
            _productDataProvider = productDataProvider;
        }

        public ProductDTO? ScrapeProductData(int webScraperId, string webAddress)
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
                    WebAddress = webAddress,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")!),
                    WebScrapingId = webScraperId
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
