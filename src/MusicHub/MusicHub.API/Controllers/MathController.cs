using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.API.Controllers
{
    [Route("math")]
    [ApiController]
    public class MathController : ControllerBase
    {

        public MathController()
        {

        }

        [HttpGet("test")]
        public IActionResult TestResult()
        {
            return Ok(new
            {
                Value = "Hello there",
                NumericValue = 17
            });
        }

        [HttpPost("post-test")]
        public ActionResult<TestResponse> TestPostResult(TestInputModel model)
        {

            return new TestResponse
            {
                Num1 = model.Num1,
                Num2 = model.Num2,
                Sum = model.Num1 + model.Num2,
                Difference = model.Num1 - model.Num2,
            };
        }

        
        public class TestInputModel
        {
            public int Num1 { get; set; }
            public int Num2 { get; set; }
        }

        public class TestResponse
        {
            public int Num1 { get; set; }
            public int Num2 { get; set; }
            public int Sum { get; set; }
            public int Difference { get; set; }
        }

    }
}
