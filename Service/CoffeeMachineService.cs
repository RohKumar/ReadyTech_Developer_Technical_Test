using System.Globalization;
using BusinessEntities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service.WebApi;

namespace Service;

public class CoffeeMachineService : ICoffeeMachineService
{
    private ILogger<CoffeeMachineService> _logger;
    private IWebApiRequest _webApiRequest;
    public CoffeeMachineService(ILogger<CoffeeMachineService> logger, IWebApiRequest webApiRequest)
    {
        _logger = logger;
        _webApiRequest = webApiRequest;
    }

    /// <summary>
    /// Returns brew coffee.
    /// </summary>
    /// <returns></returns>
    public object GetBrewCoffee()
    {
        object response = new object();
       
        try
        {
            double tempreture = GetCurrentTempreture(Constants.MelbourneLatitude, Constants.MelbourneLongitude);

            BrewCoffeeResponse brewCoffee = new BrewCoffeeResponse();
            brewCoffee.Message = "Your piping hot coffee is ready";
            brewCoffee.Prepared = DateTime.Now.ToString(Constants.ISO8601DateFormat);
            response = brewCoffee;

            if (tempreture > Constants.TempreatureThreshold) {
                response = "Your refreshing iced coffee is ready";
            }
        }
        catch(Exception e)
        {
            //logging the exception
            _logger.LogError($"error while processing request : {e.Message}");
        }

        return response;
    }

    public double GetCurrentTempreture(string latitude, string longitude)
    {
        string apiResponse= _webApiRequest.GetCurrentWeather(latitude,longitude);
        WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(apiResponse);

        return weather.main.temp;
    }
}

