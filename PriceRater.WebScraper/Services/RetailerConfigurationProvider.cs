using PriceRater.WebScraper.Utilities.Configuration;
using PriceRater.WebScraper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Services
{
    public class RetailerConfigurationProvider : IRetailerConfigurationProvider
    {
        private readonly string _solutionRoot;
        private readonly IRetailerProvider _retailerProvider; 
        
        public RetailerConfigurationProvider(IConfiguration configuration, IRetailerProvider retailerProvider)
        {
            _solutionRoot = configuration["SolutionRoot"]!;
            _retailerProvider = retailerProvider;
        }

        /// <summary>
        /// Gets the retailer configuration, configuration provides the css properties and other information for web scraping
        /// </summary>
        /// <param name="webAddress">The full web address of the product to be scraped</param>
        /// <returns></returns>
        public async Task<IConfiguration?> GetRetailerConfiguration(string webAddress)
        {
            var appSettingsInformation = ConfigProvider.GetConfiguration(_solutionRoot, "appsettings.json");
            var retailerName = await _retailerProvider.GetRetailerFromWebAddress(webAddress);

            var retailerConfigurationPath = appSettingsInformation[$"RetailerConfigurationPath:{retailerName}"]!;

            return ConfigProvider.GetConfiguration(_solutionRoot, retailerConfigurationPath);
        }
    }
}
