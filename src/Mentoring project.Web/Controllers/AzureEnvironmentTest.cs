using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace MentoringProject.Controllers
{
    [Route("api/env")]
    [ApiController]
    public class AzureEnvironmentTest : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AzureEnvironmentTest(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var message = Environment.GetEnvironmentVariable("MY_MESSAGE");
            return Ok(message+"3445");
        }

        [HttpGet]
        [Route("getDb")]
        public IActionResult GetConnectionString()
        {
            var db = _configuration.GetConnectionString("DbConnection");
            return Ok(db);
        }

        [HttpGet]
        [Route("getKeyVault")]
        public IActionResult GetMyKeyVault()
        {
            var value = _configuration["mypersonalkeyvault"];
            return Ok(value);
        }

        [HttpGet]
        [Route("getGuid")]
        public IActionResult GetGuid()
        {
            return Ok(Guid.NewGuid().ToString());
        }
    }
}
