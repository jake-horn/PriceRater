namespace PriceRater.WebScraper.Interfaces
{
    public interface IRetailerProvider
    {
        public Task<string> GetRetailerFromWebAddress(string webAddress);
    }
}