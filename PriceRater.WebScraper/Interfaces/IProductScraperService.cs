using Microsoft.Extensions.Configuration;
using PriceRater.WebScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IProductScraperService
    {
        ProductData ProvideProductData(IConfiguration retailerConfig, string webAddress);
    }
}
