using DataServer.Connections;
using DataServer.Dto;
using DataServer.Dto.Models;
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
        
        public async Task<BaseResponse> GetAll(string databaseName, string tableName, string columnNames)
        {
            try
            {
                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    columnNames = columnNames == null ? "*" : columnNames;
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

        public async Task<BaseResponse> GetByKey(string databaseName, string tableName, string key, string value, string columnNames)
        {
            try
            {
                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    columnNames = columnNames == null ? "*" : columnNames;
                    string query = $"SELECT {columnNames} from {databaseName}.{tableName} WHERE {key}=\'{value}\'";
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

        public async Task<BaseResponse> InsertRow(string databaseName, string tableName, InsertDto data)
        {
            try
            {
                // format the column and values to sql executable query
                var colandValues = data.FormatDataSets();
                var columns = colandValues[0];
                var values = colandValues[1];

                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    string query = $"INSERT INTO {databaseName}.{tableName} ({columns}) VALUES ({values})";
                    await virtuosoConn.ExecuteNonQuery(query);
                    return new BaseResponse() { Successful = true, Message = "Successfully inserted row"};
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Successful = false, Message = ex.Message };
            }
        }

        public async Task<BaseResponse> UpdateRow(string databaseName, string tableName, string id, string columnNames, string values)
        {
            try
            {
                // format the column and values to sql executable query
                var updatingColumns = FormatUpdateValues(columnNames, values);
                using (VirtuosoSqlConnection virtuosoConn = new VirtuosoSqlConnection(_config))
                {
                    string query = $"UPDATE {databaseName}.{tableName} SET {updatingColumns} WHERE id = \'{id}\'";
                    await virtuosoConn.ExecuteNonQuery(query);
                    return new BaseResponse() { Successful = true, Message = "Successfully inserted row" };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Successful = false, Message = ex.Message };
            }
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


        /// <summary>
        /// Formats the comma seperated column names and value names into the appropriate sql set values
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="valueNames"></param>
        /// <returns></returns>
        private string FormatUpdateValues(string columnNames, string valueNames)
        {
            string result = "";
            var columnList = columnNames.Split(",");
            var valueList = valueNames.Split(",");
            for (int i = 0; i < columnList.Length && i < valueList.Length; i++)
            {
                result += $" {columnList[i]} = \'{valueList[i]}\',";
            }
            return result.TrimEnd(',');
        }
    }

}
