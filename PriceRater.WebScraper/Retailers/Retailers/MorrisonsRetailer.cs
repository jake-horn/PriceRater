namespace PriceRater.WebScraper.Retailers.Retailers
{
    public class MorrisonsRetailer : RetailerBase
    {
        public override string RetailerName => "Morrisons";

        public override bool MatchedAddress(string webAddress)
        {
            return webAddress.Contains("morrisons.com");
        }
    }
}
