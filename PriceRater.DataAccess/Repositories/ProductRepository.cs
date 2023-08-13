using Dapper;
using PriceRater.DataAccess.DTO;
using PriceRater.DataAccess.Interfaces;
using System.Data;

namespace PriceRater.DataAccess.Repositories
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
                using (var connection = _connectionFactory.CreateConnection())
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

        public void UpdateProduct(ProductDTO product)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    connection.Open();

                    connection.Execute("dbo.spUpdateExistingProduct", product, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    Console.WriteLine($"Updated {product.Title} in database");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public bool DoesProductExist(ProductDTO product)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    var parameters = new { WebScrapingId = product.WebScrapingId };

                    connection.Open();

                    bool doesProductExist = connection.QuerySingle<bool>("dbo.spCheckIfProductExists", parameters, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    return doesProductExist; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false; 
            }
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    connection.Open();

                    var products = connection.Query<ProductDTO>("dbo.GetAllProducts", commandType: CommandType.StoredProcedure).ToList();

                    return products; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public IEnumerable<UserCategoryDTO> GetCategoriesAndProducts(int userId)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    connection.Open();

                    var parameters = new { UserId = userId };

                    var categories = connection.Query<UserCategoryDTO>("dbo.spGetCategoriesAndProductsForUser", parameters, commandType: CommandType.StoredProcedure).ToList();

                    return categories;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }
    }
}
