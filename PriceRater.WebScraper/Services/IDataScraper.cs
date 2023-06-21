using PriceRater.WebScraper.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Services
{
    public interface IDataScraper
    {
        public ProductDTO? ScrapeProductData(int webScraperId, string webAddress);
    }
}
