
using PriceRater.Common.Models;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IProductProviderService
    {
        public ProductDTO? GetProductData(string webAddress);
    }
}
