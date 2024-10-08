using Microsoft.AspNetCore.Mvc;
using MyCalcApp.Services;

namespace MyCalcApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        private readonly CalcService _calcService;

        public CalcController(CalcService calcService)
        {
            _calcService = calcService;
        }

        [HttpGet("add")]
        public int Add(int a, int b)
        {
            return _calcService.Add(a, b);
        }

        [HttpGet("subtract")]
        public int Subtract(int a, int b)
        {
            return _calcService.Subtract(a, b);
        }

        [HttpGet("multiply")]
        public int Multiply(int a, int b)
        {
            return _calcService.Multiply(a, b);
        }

        [HttpGet("divide")]
        public ActionResult<int> Divide(int a, int b)
        {
            try
            {
                return _calcService.Divide(a, b);
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

