using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IScraperController
    {
        public void ScrapeSingleProduct(string webAddress);
        public void ScrapeMultipleProduct(IEnumerable<string> webAddresses);
    }
}
