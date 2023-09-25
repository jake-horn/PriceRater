using Dapper;
using PriceRater.Common.Models;
using PriceRater.DataAccess.Interfaces;
using System.Data;

namespace PriceRater.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public User Create(User user)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    var parameters = new 
                    {
                        Name = user.Name, 
                        Email = user.Email, 
                        Password = user.Password,
                        UserId = 0
                    };

                    connection.Open();

                    var createdUserResult = connection.QuerySingleOrDefault<User>("auth.spCreateUser", parameters, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    return createdUserResult;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public User GetUserByEmail(string emailAddress)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    var parameters = new
                    {
                        EmailAddress = emailAddress
                    };

                    connection.Open();

                    var queriedUser = connection.QuerySingleOrDefault<User>("auth.spGetUserByEmail", parameters, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    return queriedUser;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    var parameters = new
                    {
                        Id = id
                    };

                    connection.Open();

                    return connection.QuerySingleOrDefault<User>("auth.spGetUserById", parameters, commandType: CommandType.StoredProcedure);
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
