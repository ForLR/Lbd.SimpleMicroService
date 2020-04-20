using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LBD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        public IActionResult Get()
        {
            Console.WriteLine(DateTime.Now);
            return new JsonResult(DateTime.Now);
        }

        [Route("TestException")]
        public IActionResult TestException()
        {
            Console.WriteLine("异常"+DateTime.Now);
            throw new Exception("");
        }

    }
}