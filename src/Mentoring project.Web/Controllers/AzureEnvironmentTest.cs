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
        private readonly IConfiguration configuration;
        public AzureEnvironmentTest(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var message = Environment.GetEnvironmentVariable("MY_MESSAGE");
            return Ok(message);
        }

        //[HttpGet]
        //[Route("getDb")]
        //public IActionResult GetConnectionString()
        //{
        //    var db = configuration.GetConnectionString("DbMentorship");
        //    return Ok(db);
        //}

        [HttpGet]
        [Route("getGuid")]
        public IActionResult GetGuid()
        {
            return Ok(Guid.NewGuid().ToString());
        }
    }
}
