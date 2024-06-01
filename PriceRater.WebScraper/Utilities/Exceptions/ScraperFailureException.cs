namespace PriceRater.WebScraper.Utilities.Exceptions
{
    public class ScraperFailureException : Exception
    {
        public ScraperFailureException() { }

        public ScraperFailureException(string message) : base(message) { }

        public ScraperFailureException(string message, Exception innerException) : base(message, innerException) { }
    }
}
