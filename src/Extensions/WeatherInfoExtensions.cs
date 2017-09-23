using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Model;

namespace AspNetCoreIHostedService.Extensions
{
    public static class WeatherInfoExtensions
    {
        public static WeatherData ToWeatherData(this WeatherInfo weatherInfo)
        {
            return new WeatherData()
            {
                CityName = weatherInfo.name,
                Humidity =  weatherInfo.main.humidity,
                Pressure = weatherInfo.main.pressure,
                Temperature = weatherInfo.main.temp,
                Lat = weatherInfo.coord.lat,
                Lon = weatherInfo.coord.lon,
                MainWeather = weatherInfo.weather.First().main,
                WeatherDescription = weatherInfo.weather.First().description
            };
        }
    }
}
