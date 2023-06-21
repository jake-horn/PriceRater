using Microsoft.Extensions.Configuration;

namespace PriceRater.WebScraper.Services
{
    public interface IWebScraperService
    { 
        public IConfiguration? GetRetailerConfiguration(string webAddress);
    }
}
