using DataServer.Connections;
using DataServer.Services.Contracts;

namespace DataServer.Services.Implementation
{
    public class SqlService : ISqlService
    {
        private readonly IConfiguration _config;
        public SqlService(IConfiguration config)
        {
            _config = config;
        }
        public Task<string> DeleteOne(string databaseName, string tableName, string key)
        {
            throw new NotImplementedException();
        }
        
        public async Task<string> GetAll(string databaseName, string tableName)
        {
            using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
            {
                var reader = virtuosoConn.ExecuteReader("select * from approovia_db.approovia.userstest");
                return "";
            }
        }

        public Task<string> GetByKey(string databaseName, string tableName, string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> InsertOne(string databaseName, string tableName, string body)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateOne(string databaseName, string tableName, string key, string model)
        {
            throw new NotImplementedException();
        }
    }
}
