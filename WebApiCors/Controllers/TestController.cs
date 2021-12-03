using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("client1")]

    public class TestController : ControllerBase
    {
        IOptions<CorsOptions> _options;

        public TestController(IOptions<CorsOptions> options)
        {
            _options = options;

            
        }


        [EnableCors("client1")]
        public ActionResult GetProducts()
        {
            return Ok("Client1");

        }

        [HttpPost]
        [EnableCors("client1")]
        public ActionResult Post([FromBody] string value)
        {
            return Ok("Posted");
        }


        [HttpPut]
        [EnableCors("client2")]
        public ActionResult Put([FromBody] string value)
        {
            return Ok("PUT");
        }
    }
}
