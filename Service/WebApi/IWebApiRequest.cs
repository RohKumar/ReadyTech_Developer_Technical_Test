using System;
namespace Service.WebApi
{
	public interface IWebApiRequest
	{
		string GetCurrentWeather(string latitude,string longitude);
	}
}

