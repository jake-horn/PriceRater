using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper.Retailers
{
    public abstract class RetailerBase : IRetailer
    {
        public abstract string RetailerName { get; }

        public abstract bool MatchedAddress(string webAddress);
    }
}
