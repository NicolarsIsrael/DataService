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
    public class SqlController : ControllerBase
    {
        private readonly ISqlService _sqlService;
        public SqlController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet(Name = "select-all")]
        public async Task<string> Select(string databaseName, string tableName)
        {
            //var databaseName = "approovia_db";
            //var tableName = "approovia.userstest";
            var reader = await _sqlService.GetAll(databaseName, tableName);
            return reader;
        }

        //[HttpGet(Name ="insert")]
        //public async Task<string> Insert([FromRoute]dynamic body)
        //{

        //    return body.ToString();
        //}

    }

}
