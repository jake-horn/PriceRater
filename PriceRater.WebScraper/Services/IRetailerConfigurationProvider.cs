using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Services
{
    public interface IRetailerConfigurationProvider
    { 
        public IConfiguration? GetRetailerConfiguration(string webAddress);
    }
}
