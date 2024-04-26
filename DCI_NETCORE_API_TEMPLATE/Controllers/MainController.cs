using DCI_NETCORE_API_TEMPLATE.Models;
using Microsoft.AspNetCore.Mvc;

namespace DCI_NETCORE_API_TEMPLATE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : Controller
    {
        private OraConnectDB ORA = new OraConnectDB("ORA_DATABASE_NAME");
        private SqlConnectDB SQL = new SqlConnectDB("SQL_DATABASE_NAME");

        public MainController(OraConnectDB oRA, SqlConnectDB sQL)
        {
            ORA = oRA;
            SQL = sQL;
        }

        [HttpGet]
        [Route("/get")]
        public IActionResult GetData(string param)
        {

            return Ok();
        }

        [HttpPost]
        [Route("/post")]
        public IActionResult PostData([FromBody] ModelExample param)
        {
            return Ok();
        }
    }
}
