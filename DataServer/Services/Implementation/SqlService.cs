using DataServer.Connections;
using DataServer.Services.Contracts;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Odbc;
using System.Reflection;

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
                string query = $"SELECT * from {databaseName}.{tableName}";
                var reader = virtuosoConn.ExecuteReader(query);
                return SerializeToObject(reader);
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

        /// <summary>
        /// Convert reader to object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private string SerializeToObject(OdbcDataReader reader)
        {
            List<object> objects = new List<object>();
            while (reader.Read())
            {
                IDictionary<string, object> record = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    record.Add(reader.GetName(i), reader[i]);
                }
                objects.Add(record);
            }
            var json = JsonConvert.SerializeObject(objects);
            return json;
        }

    }

}
