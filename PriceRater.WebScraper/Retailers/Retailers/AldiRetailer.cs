namespace PriceRater.WebScraper.Retailers.Retailers
{
    public class AldiRetailer : RetailerBase
    {
        public override string RetailerName => "Aldi";

        public override bool MatchedAddress(string webAddress)
        {
            return webAddress.Contains("aldi.co.uk");
        }
    }
}
