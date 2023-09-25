using PriceRater.Common.Models;
using PriceRater.DataAccess.DTO;

namespace PriceRater.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(ProductDTO product);

        public void UpdateProduct(ProductDTO product);

        public IEnumerable<ProductDTO> GetProducts();

        public IEnumerable<UserCategoryDTO> GetCategoriesAndProducts(int userId);
    }
}
