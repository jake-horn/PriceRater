using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IRetailerConfigurationProvider
    {
        public Task<IConfiguration?> GetRetailerConfiguration(string webAddress);
    }
}
