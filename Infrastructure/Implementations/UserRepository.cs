using Application;
using Core;
using Dapper;
using Infrastructure.Helper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class UserRepository : IUserRepository
    {
        private static IConfiguration _configuration; 
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetUserListAsync()
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                var result = connection.QueryAsync<User>(SqlConstants.GetUserlist, null, commandType: CommandType.StoredProcedure).Result;

                return result;
            }
        }

        public async Task<User> GetUserByCodeAsync(long id)
        {
            using (IDbConnection connection = GetOpenConnection())
            {
                var result = connection.QuerySingleOrDefault<User>(SqlConstants.GetUserByCode, new { @id = id }, commandType: CommandType.StoredProcedure);

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
