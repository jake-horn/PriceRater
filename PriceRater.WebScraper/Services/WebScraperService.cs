using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Services
{
    public class WebScraperService : IWebScraperService
    {
        private readonly string solutionRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        
        public WebScraperService()
        {
        }

        public IConfiguration? GetRetailerConfiguration(string webAddress)
        {
            string retailerName; 
            string retailerConfigurationPath; 
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

            retailerConfigurationPath = appSettingsInformation.GetSection("RetailerConfigurationPath")[retailerName];

            return GetConfiguration(solutionRoot, retailerConfigurationPath);
        }

        private IConfiguration GetConfiguration(string solutionRoot, string jsonFilePath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(solutionRoot)
                .AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
