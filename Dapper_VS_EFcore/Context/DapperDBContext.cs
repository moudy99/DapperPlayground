using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper_VS_EFcore.Context
{
    public class DapperDBContext
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public DapperDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection GetConnection()
         => new SqlConnection(_connectionString);
        
    }
}
