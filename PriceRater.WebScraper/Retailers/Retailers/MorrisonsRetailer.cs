using PriceRater.WebScraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Retailers.Retailers
{
    public class MorrisonsRetailer : IRetailer
    {
        public string RetailerName => "Morrisons";

        public bool MatchedAddress(string webAddress)
        {
            return webAddress.Contains("morrisons.com");
        }
    }
}
