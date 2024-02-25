using System.Globalization;
using BusinessEntities;
using Microsoft.Extensions.Logging;

namespace Service;

public class CoffeeMachineService : ICoffeeMachineService
{
    private ILogger<CoffeeMachineService> _logger;
    public CoffeeMachineService(ILogger<CoffeeMachineService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns brew coffee.
    /// </summary>
    /// <returns></returns>
    public BrewCoffeeResponse GetBrewCoffee()
    {
        BrewCoffeeResponse brewCoffee = new BrewCoffeeResponse();
        try
        {
            // Date format is ISO-8601
            brewCoffee.Prepared = DateTime.Now.ToString(Constants.ISO8601DateFormat);
            brewCoffee.Message = "Your piping hot coffee is ready";
        }
        catch(Exception e)
        {
            //logging the exception
            _logger.LogError($"error while processing request : {e.Message}");
        }
        return brewCoffee;
    }
}

