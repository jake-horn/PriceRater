using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Services
{
    public class WebScraperService : IWebScraperService
    {
        private readonly string? solutionRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        
        public WebScraperService()
        {
        }

        /// <summary>
        /// Gets the retailer configuration, configuration provides the css properties and other information for web scraping
        /// </summary>
        /// <param name="webAddress">The full web address of the product to be scraped</param>
        /// <returns></returns>
        public IConfiguration? GetRetailerConfiguration(string webAddress)
        {
            string retailerName; 
            var appSettingsInformation = GetConfiguration(solutionRoot, "appsettings.json");

            switch (webAddress)
            {
                case string asdaAddress when asdaAddress.Contains("asda.com"):
                    retailerName = "Asda"; 
                    break;
                case string aldiAddress when aldiAddress.Contains("aldi.co.uk"):
                    retailerName = "Aldi";
                    break;
                default:
                    return null;
            }

            var retailerConfigurationPath = appSettingsInformation.GetSection("RetailerConfigurationPath")[retailerName];

            return GetConfiguration(solutionRoot, retailerConfigurationPath);
        }

        /// <summary>
        /// Returns the configuration for the retailer depending on the file path provided in the parameter
        /// </summary>
        /// <param name="solutionRoot">Root of the solution, to locate the json file</param>
        /// <param name="jsonFilePath">File path for the json configuration</param>
        /// <returns></returns>
        private IConfiguration GetConfiguration(string solutionRoot, string jsonFilePath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(solutionRoot)
                .AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
