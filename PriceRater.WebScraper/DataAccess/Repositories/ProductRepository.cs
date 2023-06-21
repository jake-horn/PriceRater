using Dapper;
using PriceRater.WebScraper.DataAccess.DTO;
using PriceRater.WebScraper.DataAccess.Interfaces;
using System.Data;

namespace PriceRater.WebScraper.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _connectionFactory; 

        public ProductRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void AddProduct(ProductDTO product)
        {
            try
            {
                using (var connection =  _connectionFactory.CreateConnection())
                {
                    connection.Open();

                    connection.Execute("dbo.spAddProduct", product, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    Console.WriteLine($"Added {product.Title} to database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
