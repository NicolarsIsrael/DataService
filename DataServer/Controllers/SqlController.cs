//using DataServer.Services.Contracts;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace DataServer.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SqlController : ControllerBase
//    {
//        private readonly ISqlService _sqlService;
//        public SqlController(ISqlService sqlService)
//        {
//            _sqlService = sqlService;
//        }

//        public IActionResult SelectAll()
//        {
//            var reader = _sqlService.GetAll("", "");
//            return Ok();
//        }
//    }
//}
