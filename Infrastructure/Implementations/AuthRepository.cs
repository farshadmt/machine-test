using Application;
using Core;
using Dapper;
using Infrastructure.Helper;
using Microsoft.Extensions.Configuration; 
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AuthRepository : IAuthRepository
    {
        private static IConfiguration _configuration;
        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> RegisterUserAsync(User user)
        { 
            using (IDbConnection connection = GetOpenConnection())
            { 
                var result = connection.QuerySingleOrDefaultAsync<User>(SqlConstants.UserRegistration, new { @Data = Newtonsoft.Json.JsonConvert.SerializeObject(user) }, commandType: CommandType.StoredProcedure).Result;

                return result;
            }
        }

        public async Task<User> LoginAsync(Login login)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                var result = connection.QuerySingleOrDefaultAsync<User>(SqlConstants.UserLogin, new { @Data = Newtonsoft.Json.JsonConvert.SerializeObject(login) }, commandType: CommandType.StoredProcedure).Result;

                return result;
            }
        }

        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            return connection;
        }
    }
}
