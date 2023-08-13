using PriceRater.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(ProductDTO product);

        public void UpdateProduct(ProductDTO product);

        public bool DoesProductExist(ProductDTO product);

        public IEnumerable<ProductDTO> GetProducts();

        public IEnumerable<UserCategoryDTO> GetCategoriesAndProducts(int userId);
    }
}
