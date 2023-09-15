using PriceRater.WebScraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Retailers.Retailers
{
    public class AsdaRetailer : IRetailer
    {
        public string RetailerName => "Asda"; 

        public bool MatchedAddress(string webAddress)
        {
            return webAddress.Contains("asda.com");
        }
    }
}
