namespace PriceRater.WebScraper.Retailers.Retailers
{
    public class TescoRetailer : RetailerBase
    {
        public override string RetailerName => "Tesco";

        public override bool MatchedAddress(string webAddress)
        {
            return webAddress.Contains("tesco.com");
        }
    }
}
