using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IRetailerConfigurationProvider
    {
        public IConfiguration? GetRetailerConfiguration(string webAddress);
    }
}
