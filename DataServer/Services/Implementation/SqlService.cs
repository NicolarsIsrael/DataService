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
        
        public async Task<List<Person>> GetAll(string databaseName, string tableName)
        {
            OdbcDataReader reader = null;
            using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
            {
                string query = $"SELECT * from {databaseName}.{tableName}";
                reader = virtuosoConn.ExecuteReader(query);
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
        /// Convert
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<Person> SerializeToObject(OdbcDataReader reader)
        {
            var persons = new List<Person>();
            while (reader.Read())
            {
                var person = new Person();

                PropertyInfo[] properties = typeof(Person).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        property.SetValue(person, reader.GetValue(reader.GetOrdinal(property.Name)));
                    }
                    catch (Exception)
                    {
                        property.SetValue(person, null);
                    }
                }
                persons.Add(person);
            }
            return persons;
        }

    }

    public class Person
    {
        public string userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string Secret { get; set; }
        public string Country { get; set; }
    }
}
