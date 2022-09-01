using DataServer.Dto;
using DataServer.Services.Contracts;
using DataServer.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("select-all")]
        public async Task<IActionResult> SelectAll(string databaseName, string tableName)
        {
            var result = await _sqlService.GetAll(databaseName, tableName);
            return Ok(result);
        }

        [HttpGet]
        [Route("select-columns")]
        public async Task<IActionResult> SelectColumns(string databaseName, string tableName, string columnNames)
        {
            var result = await _sqlService.GetAllByColumns(databaseName, tableName, columnNames);
            return Ok(result);
        }

        [HttpDelete]
        [Route("delete-row")]
        public async Task<IActionResult> DeleteRow(string databaseName, string tableName, string id)
        {
            var result = await _sqlService.DeleteRow(databaseName, tableName, id);
            return Ok(result);
        }

        //[HttpGet(Name ="insert")]
        //public async Task<string> Insert([FromRoute]dynamic body)
        //{

        //    return body.ToString();
        //}

    }

}
