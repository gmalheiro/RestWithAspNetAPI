using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithAspNetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Addition/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Addition(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDouble(firstNumber) + ConvertToDouble(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("Subtraction/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Subtraction(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = ConvertToDouble(firstNumber) - ConvertToDouble(secondNumber);
                return Ok(result.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("Multiplication/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Multiplication(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = ConvertToDouble(firstNumber) * ConvertToDouble(secondNumber);
                return Ok(result.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("Division/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Division(string firstNumber, string secondNumber)
        {
            if (secondNumber != "0")
            {
                if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                {
                    var result = ConvertToDouble(firstNumber); ConvertToDouble(secondNumber);
                    return Ok(result.ToString());
                }
                return BadRequest("Invalid Input");
            }
            return BadRequest($"Second number: {secondNumber} must be greater than 0");
        }

        [HttpGet("Mean/{firstNumber}/{secondNumber}")]
        public ActionResult<string> Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var result = (ConvertToDouble(firstNumber) + ConvertToDouble(secondNumber)) / 2;
                return Ok(result.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("SquareRoot/{firstNumber}")]
        public ActionResult<string> SquareRoot(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var result = Math.Sqrt(ConvertToDouble(firstNumber));
                return Ok(result.ToString());
            }
            return BadRequest("Invalid Input");
        }

        private double ConvertToDouble(string strNumber)
        {
            double doubleValue;
            if (double.TryParse(strNumber, out doubleValue))
                return doubleValue;
            return 0.0;
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return isNumber;
        }
    }
}
