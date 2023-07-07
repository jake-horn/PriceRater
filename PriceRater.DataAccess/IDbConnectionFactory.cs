using System.Data;

namespace PriceRater.DataAccess
{
    public interface IDbConnectionFactory
    { 
        public IDbConnection CreateConnection();
    }
}