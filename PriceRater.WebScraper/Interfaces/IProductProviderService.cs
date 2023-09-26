
using PriceRater.Common.Models;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IProductProviderService
    {
        public Task<ProductDTO?> GetProductData(string webAddress);
    }
}
