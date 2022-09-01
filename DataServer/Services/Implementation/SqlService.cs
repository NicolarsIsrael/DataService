﻿using DataServer.Connections;
using DataServer.Dto;
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
        public async Task<BaseResponse> DeleteRow(string databaseName, string tableName, string Id)
        {
            try
            {
                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    string query = $"DELETE FROM {databaseName}.{tableName} WHERE id=\'{Id}\'";
                    await virtuosoConn.ExecuteNonQuery(query);
                    return new BaseResponse() { Successful = true, Message = "Successfully deleted row" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Successful = false, Message = ex.Message };
            }
        }
        
        public async Task<BaseResponse> GetAll(string databaseName, string tableName)
        {
            try
            {
                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    string query = $"SELECT * from {databaseName}.{tableName}";
                    var reader = virtuosoConn.ExecuteReader(query);
                    var json = SerializeToObject(reader);
                    return new BaseResponse() { Successful = true, Message = "Successful request", Content = json };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Successful = false, Message = ex.Message };
            }

        }

        public async Task<BaseResponse> GetAllByColumns(string databaseName, string tableName, string columnNames)
        {
            try
            {
                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    string query = $"SELECT {columnNames} from {databaseName}.{tableName}";
                    var reader = virtuosoConn.ExecuteReader(query);
                    var json = SerializeToObject(reader);
                    return new BaseResponse() { Successful = true, Message = "Successful request", Content = json };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Successful = false, Message = ex.Message };
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
