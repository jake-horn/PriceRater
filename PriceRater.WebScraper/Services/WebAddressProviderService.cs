using PriceRater.WebScraper.DataAccess;
using Dapper; 
using System.Data;

namespace PriceRater.WebScraper.Services
{
    public class WebAddressProviderService : IWebAddressProviderService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public WebAddressProviderService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IDictionary<int, string> GetWebAddresses()
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    connection.Open();

                    var webAddresses = connection
                        .Query("spGetAllWebScrapingAddresses", commandType: CommandType.StoredProcedure)
                        .Select(row => new KeyValuePair<int, string>((int)row.Id, (string)row.WebAddress))
                        .ToDictionary(
                            pair => pair.Key,
                            pair => pair.Value
                        );

                    connection.Close();

                    return webAddresses;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return new Dictionary<int, string>();
        }
    }
}
