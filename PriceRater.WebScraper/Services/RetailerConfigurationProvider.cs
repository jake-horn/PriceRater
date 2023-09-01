using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Services
{
    public class RetailerConfigurationProvider : IRetailerConfigurationProvider
    {
        private readonly string _solutionRoot; 
        
        public RetailerConfigurationProvider(IConfiguration configuration)
        {
            _solutionRoot = configuration["SolutionRoot"];
        }

        /// <summary>
        /// Gets the retailer configuration, configuration provides the css properties and other information for web scraping
        /// </summary>
        /// <param name="webAddress">The full web address of the product to be scraped</param>
        /// <returns></returns>
        public IConfiguration? GetRetailerConfiguration(string webAddress)
        {
            var appSettingsInformation = GetConfiguration(_solutionRoot, "appsettings.json");
            var retailerName = GetRetailerFromWebAddress(webAddress);

            var retailerConfigurationPath = appSettingsInformation.GetSection("RetailerConfigurationPath")[retailerName]!;
            var retailerConfiguration = GetConfiguration(_solutionRoot, retailerConfigurationPath);

            return retailerConfiguration;
        }

        /// <summary>
        /// Gets the retailer name from the web address, for use with retrieving the correct retailer settings
        /// </summary>
        /// <param name="webAddress">Web address of product</param>
        /// <returns>Value of retailer as a string</returns>
        private string GetRetailerFromWebAddress(string webAddress)
        {
            return webAddress switch
            {
                string asdaAddress when asdaAddress.Contains("asda.com") => "Asda",
                string aldiAddress when aldiAddress.Contains("aldi.co.uk") => "Aldi",
                string morrisonsAddress when morrisonsAddress.Contains("morrisons.com") => "Morrisons",
                _ => "Invalid",
            };
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
