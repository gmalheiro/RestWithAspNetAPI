using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Addition/{firstNumber}/{secondNumber}")]
        public IActionResult Addition(string firstNumber, string secondNumber)
        {
            return BadRequest("Invalid Input");
        }
    }
}
