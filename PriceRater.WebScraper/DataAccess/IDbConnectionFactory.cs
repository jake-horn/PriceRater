using System.Data;

namespace PriceRater.WebScraper.DataAccess
{
    public interface IDbConnectionFactory
    {
        public IDbConnection CreateConnection();
    }
}
