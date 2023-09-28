using PriceRater.Common.Models;
using PriceRater.DataAccess.Interfaces;
using PriceRater.DataAccess.Repositories;

namespace PriceRater.API.Helpers
{
    public class ProductHelpers : IProductHelpers
    {
        public void AddOrUpdateProduct(ProductDTO responseData, IProductRepository productRepository, bool doesProductExist)
        {
            if (doesProductExist)
            {
                productRepository.UpdateProduct(responseData);
            }
            else
            {
                productRepository.AddProduct(responseData);
            }
        }
    }
}
