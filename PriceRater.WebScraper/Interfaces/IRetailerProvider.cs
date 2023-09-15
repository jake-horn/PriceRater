namespace PriceRater.WebScraper.Interfaces
{
    public interface IRetailerProvider
    {
        string GetRetailerFromWebAddress(string webAddress);
    }
}