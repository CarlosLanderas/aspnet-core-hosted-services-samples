using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Extensions;
using AspNetCoreIHostedService.Model;
using Newtonsoft.Json;

namespace AspNetCoreIHostedService.Infrastructure
{
    public class WeatherClient
    {
        private const string WeatherAPIUrl = "http://api.openweathermap.org/data/2.5/weather?q=#city#&appid=c281433c547be5d11a0b8fcdb0702277";
        
        public async Task<WeatherData> GetCity(string city)

        {
            using (var client = new HttpClient())
            {
                var result =  await client.GetStringAsync(BuildRequestUrl(city));
                return JsonConvert.DeserializeObject<WeatherInfo>(result).ToWeatherData();
            }
        }

        private string BuildRequestUrl(string city)
        {
            return WeatherAPIUrl.Replace("#city#", city);
        }
    }
}
