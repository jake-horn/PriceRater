using PriceRater.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IProductProviderService
    {
        public ProductDTO? GetProductData(int webScraperId, string webAddress);
    }
}
