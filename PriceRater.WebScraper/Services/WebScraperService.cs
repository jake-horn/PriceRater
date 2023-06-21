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
            string retailerConfigurationPath; 
            var appSettingsInformation = LoadAppSettingsInformation(solutionRoot);

            switch (webAddress)
            {
                case string asdaAddress when asdaAddress.Contains("asda.com"):
                    retailerConfigurationPath = appSettingsInformation.GetSection("RetailerConfigurationPath")["Asda"];
                    break;
                case string aldiAddress when aldiAddress.Contains("aldi.co.uk"):
                    retailerConfigurationPath = appSettingsInformation.GetSection("RetailerConfigurationPath")["Aldi"];
                    break;
                default:
                    return null;
            }

            return new ConfigurationBuilder()
                .SetBasePath(solutionRoot)
                .AddJsonFile(retailerConfigurationPath, optional: true, reloadOnChange: true)
                .Build();
        }

        private IConfiguration LoadAppSettingsInformation(string solutionRoot)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(solutionRoot)
                .AddJsonFile("appsettings.json")
                .Build();

            return configurationBuilder;
        }
    }
}
