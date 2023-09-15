using PriceRater.WebScraper.Interfaces;

namespace PriceRater.WebScraper.Retailers
{
    public class RetailerProvider : IRetailerProvider
    {
        private readonly IEnumerable<IRetailer> _retailers;

        public RetailerProvider(IEnumerable<IRetailer> retailers)
        {
            _retailers = retailers;
        }

        public string GetRetailerFromWebAddress(string webAddress)
        {
            foreach (var retailer in _retailers)
            {
                if (retailer.MatchedAddress(webAddress))
                {
                    return retailer.RetailerName;
                }
            }

            return "Invalid";
        }
    }
}
