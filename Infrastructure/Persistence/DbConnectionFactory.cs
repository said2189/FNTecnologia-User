using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Persistence
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;
        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }

}
