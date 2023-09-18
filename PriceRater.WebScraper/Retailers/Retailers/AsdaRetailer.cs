namespace PriceRater.WebScraper.Retailers.Retailers
{
    public class AsdaRetailer : RetailerBase
    {
        public override string RetailerName => "Asda"; 

        public override bool MatchedAddress(string webAddress)
        {
            return webAddress.Contains("asda.com");
        }
    }
}
