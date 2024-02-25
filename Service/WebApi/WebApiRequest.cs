using System;
using BusinessEntities;
using Microsoft.Extensions.Configuration;

namespace Service.WebApi
{
	public class WebApiRequest : IWebApiRequest
	{
        private string? _apiUrl;
        private IConfiguration _configuration;

        public WebApiRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the current weather data as per latitude and longitude
        /// </summary>
        /// <returns></returns>
        public string GetCurrentWeather(string latitude, string longitude)
        {
            string response = string.Empty;
            _apiUrl = GetApiUrl(latitude, longitude);
            using (HttpClient client = new HttpClient())
            {
                var responseTask = client.GetAsync(_apiUrl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }

            return response;
        }

        /// <summary>
        /// Returns constructed url as per parameter.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        private string GetApiUrl(string latitude, string  longitude)
        {
            return  $"{_configuration[$"{Constants.WebApiSettingKey}:{Constants.ApiBaseUrlKey}"]}?lat={latitude}&lon={longitude}&appid={_configuration[$"{Constants.WebApiSettingKey}:{Constants.ApiKey}"]}&units=metric";
        }
    }
}

