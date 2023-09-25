using PriceRater.Common.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IScraperController
    {
        public ProductDTO ScrapeProduct(string webAddress);

        public IEnumerable<ProductDTO> ScrapeMultipleProducts(IEnumerable<string> webAddresses);
    }
}
