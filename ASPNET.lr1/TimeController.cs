using Microsoft.AspNetCore.Mvc;
using MyCalcApp.Services;

namespace MyCalcApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly TimeService _timeService;

        public TimeController(TimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet]
        public string GetTimeOfDay()
        {
            return _timeService.GetTimeOfDayMessage();
        }
    }
}
