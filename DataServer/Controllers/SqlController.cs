using DataServer.Dto;
using DataServer.Dto.Models;
using DataServer.Services.Contracts;
using DataServer.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Odbc;
using System.Reflection;

namespace DataServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sqlController : ControllerBase
    {
        private readonly ISqlService _sqlService;
        public sqlController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        /// <summary>
        /// The end point makes a call to the service to retrieve all items for the specified column
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll(string database, string table, string? columns)
        {
            var result = await _sqlService.GetAll(database, table, columns);
            return Ok(result);
        }


        /// <summary>
        /// The end point makes a call to the service to retireve the rows that are valid for the specified key and value
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-by-key")]
        public async Task<IActionResult> GetByKey(string database, string table, string key, string value, string? columns)
        {
            var result = await _sqlService.GetByKey(database, table, key, value, columns);
            return Ok(result);
        }

        /// <summary>
        /// The end point makes a call to the service to delete a specified row
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-row")]
        public async Task<IActionResult> DeleteRow(string database, string table, string id)
        {
            var result = await _sqlService.DeleteRow(database, table, id);
            return Ok(result);
        }

        /// <summary>
        /// The end point makes a call to the service to add a row to a database table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-row")]
        public async Task<IActionResult> AddRow(string database, string table, [FromBody] InsertRowDto data)
        {
            var result = await _sqlService.InsertRow(database.ToLower(), table, data);
            return Ok(result);
        }

        /// <summary>
        /// The end point makes a call to the service to update a specified row
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-row")]
        public async Task<IActionResult> UpdateRow(string database, string table, [FromBody] UpdateRowDto data)
        {
            var result = await _sqlService.UpdateRow(database, table, data);
            return Ok(result);
        }


        /// <summary>
        /// The end point makes a call to the service to create a database table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-table")]
        public async Task<IActionResult> CreateTable(string database, string table, [FromBody] CreateTableDto data)
        {
            var result = await (_sqlService.CreateTable(database.ToLower(), table, data));
            return Ok(result);
        }

        /// <summary>
        /// The end point makes a call to the service to add a column to a database table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-column")]
        public async Task<IActionResult> AddColumn(string database, string table, AddColumnDto data)
        {
            var result = await (_sqlService.AddColumn(database, table, data));
            return Ok(result);
        }

        /// <summary>
        /// The end point makes a call to the service to delete a column from a database table
        /// </summary>
        /// <param name="database"></param>
        /// <param name="table"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-column")]
        public async Task<IActionResult> DeleteColumn(string database, string table, DeleteColumnDto data)
        {
            var result = await (_sqlService.DeleteColumn(database, table, data));
            return Ok(result);
        }
    }

}
