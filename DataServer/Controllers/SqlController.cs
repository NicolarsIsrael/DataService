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
            //var databaseName = "approovia_db";
            //var tableName = "approovia.userstest";
            var json = await _sqlService.GetAll(databaseName, tableName);
            return Ok(json);
        }

        [HttpGet]
        [Route("select-columns")]
        public async Task<IActionResult> SelectColumns(string databaseName, string tableName, string columnNames)
        {
            var json = await _sqlService.GetAllByColumns(databaseName, tableName, columnNames);
            return Ok(json);
        }

        //[HttpGet(Name ="insert")]
        //public async Task<string> Insert([FromRoute]dynamic body)
        //{

        //    return body.ToString();
        //}

    }

}
