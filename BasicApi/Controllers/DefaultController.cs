using Microsoft.AspNetCore.Mvc;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var anonymousType = new
            {
                Message = "Welcome to the machine",
                Now = DateTime.Now.ToString()
            };

            return Ok(anonymousType);
        }
    }
}
