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

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll(string databaseName, string tableName, string? columnNames)
        {
            var result = await _sqlService.GetAll(databaseName, tableName, columnNames);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-key")]
        public async Task<IActionResult> GetByKey(string databaseName, string tableName, string key, string value, string? columnNames)
        {
            var result = await _sqlService.GetByKey(databaseName, tableName, key, value, columnNames);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-row")]
        public async Task<IActionResult> DeleteRow(string databaseName, string tableName, string id)
        {
            var result = await _sqlService.DeleteRow(databaseName, tableName, id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add-row")]
        public async Task<IActionResult> Add(string databaseName, string tableName, [FromBody] InsertRowDto data)
        {
            var result = await _sqlService.InsertRow(databaseName.ToLower(), tableName, data);
            return Ok(result);
        }

        [HttpPut]
        [Route("update-row")]
        public async Task<IActionResult> Update(string databaseName, string tableName, [FromBody] UpdateRowDto data)
        {
            var result = await _sqlService.UpdateRow(databaseName, tableName, data);
            return Ok(result);
        }

        [HttpPost]
        [Route("create-table")]
        public async Task<IActionResult> CreateTable(string databaseName, string tableName, [FromBody] CreateTableDto data)
        {
            var result = await (_sqlService.CreateTable(databaseName.ToLower(), tableName, data));
            return Ok(result);
        }
    }

}
