using PriceRater.Common.Models;
using PriceRater.DataAccess.Interfaces;

namespace PriceRater.API.Helpers
{
    public interface IProductHelpers
    {
        public void AddOrUpdateProduct(ProductDTO responseData, IProductRepository productRepository, bool doesProductExist);
    }
}
