using PriceRater.WebScraper.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.DataAccess.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(ProductDTO product);
    }
}
