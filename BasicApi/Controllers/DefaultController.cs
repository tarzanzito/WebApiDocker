using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class DefaultController : ControllerBase
    {

        //private readonly ILogger<WeatherForecastController> _logger;

        //public BasicController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        public DefaultController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            var anonymousType = new
            {
                Message = "Welcome to the machine",
                Now = DateTime.Now.ToString()
            };
            return this.Ok(anonymousType);

        }
    }
}
