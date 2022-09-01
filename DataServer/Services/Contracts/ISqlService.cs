﻿using DataServer.Dto;
using DataServer.Services.Implementation;
using System.Data.Odbc;

namespace DataServer.Services.Contracts
{
    public interface ISqlService
    {
        /// <summary>
        /// Retrieve all items from a table in a particular database
        /// </summary>
        /// <param name="databaseName">Name of the database where the table can be found</param>
        /// <param name="tableName">Name of the table where the items reside in</param>
        /// <param name="columnNames">Names of the columns to be retrieved. By default * to retrieve all columns</param>
        /// <returns></returns>
        Task<BaseResponse> GetAll(string databaseName, string tableName, string columnNames = "*");


        /// <summary>
        /// Retrieve a particular item by the specified key. The item is searched for in the specified table and database provided
        /// </summary>
        /// <param name="databaseName">Database name where the item is expected to be found</param>
        /// <param name="tableName">Table where the item is expected to be found</param>
        /// <param name="key">Parameter to which the item can be identified by</param>
        /// <param name="columnNames">Names of the columns to be retrieved. By default * to retrieve all columns</param>
        /// <returns></returns>
        Task<BaseResponse> GetByKey(string databaseName, string tableName, string key, string value, string columnNames = "*");

        /// <summary>
        /// Inserts a row into the table and database specified
        /// </summary>
        /// <param name="databaseName">Name of the database to which the item will be placed</param>
        /// <param name="tableName">Name of the table to which the item will be appended to</param>
        /// <param name="data">odel details about the item</param>
        /// <returns></returns>
        Task<BaseResponse> InsertRow(string databaseName, string tableName, string columnNames, string values);

        /// <summary>
        /// Removes an item from the database. This item is uniquely identified by the key, table name and database
        /// </summary>
        /// <param name="databaseName">Name of the database where the item resides</param>
        /// <param name="tableName">Name of the table where the item resides</param>
        /// <param name="key">Name of the key to uniquely identify the item in the table</param>
        /// <returns></returns>
        Task<BaseResponse> DeleteRow(string databaseName, string tableName, string Id);

        /// <summary>
        /// Update the details of an item in the specified table and database
        /// </summary>
        /// <param name="databaseName">Name of the database where the item is located</param>
        /// <param name="tableName">Name of the table where the item resides</param>
        /// <param name="key">Name of the key to uniquely identify the time in the table</param>
        /// <param name="model">Request parameter change</param>
        /// <returns></returns>
        Task<string> UpdateOne(string databaseName, string tableName, string key, string model);
    }
}
