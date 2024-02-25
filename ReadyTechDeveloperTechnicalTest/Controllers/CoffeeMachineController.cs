using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Service;


namespace ReadyTechDeveloperTechnicalTest.Controllers
{
    public class CoffeeMachineController : Controller
    {
        private ICoffeeMachineService _coffeeMachineService;

        /// <summary>
        /// Constructor initialization.
        /// </summary>
        /// <param name="coffeeMachineService"></param>
        public CoffeeMachineController(ICoffeeMachineService coffeeMachineService)
        {
            _coffeeMachineService = coffeeMachineService;
        }

        /// <summary>
        /// Returns a brew coffee
        /// if we want to check for 1'st April we can pass the date as parameter.for eg "brew-coffee/2024-04-01"to get message "418 I'm a teapot"
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("brew-coffee/{today?}")]
        public IActionResult GetBrewCoffee(DateTime? today)
        {
            if (CommonService.IsFirstApril(today))
            {
                return StatusCode(StatusCodes.Status418ImATeapot, $"{StatusCodes.Status418ImATeapot} {ReasonPhrases.GetReasonPhrase(StatusCodes.Status418ImATeapot)}");
            }
            else
            {
                StringBuilder responseFormat = new StringBuilder();
                responseFormat.Append("{");
                responseFormat.Append("\r\n");
                responseFormat.Append("“message”: “");
                responseFormat.Append(_coffeeMachineService.GetBrewCoffee().Message);
                responseFormat.Append("\"");
                responseFormat.Append(",");
                responseFormat.Append("\r\n");
                responseFormat.Append("“prepared”: “");
                responseFormat.Append(_coffeeMachineService.GetBrewCoffee().Prepared);
                responseFormat.Append("\"");
                responseFormat.Append("\r\n");
                responseFormat.Append("}");
                return Ok(responseFormat.ToString());
            }
               
        }
    }
}

