using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIHostedService.Model
{
    public class WeatherData
    {
        public string CityName { get; set; }
        public double Lon { get; set;  }
        public double Lat { get; set; }
        public string MainWeather { get; set; }
        public string WeatherDescription { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
    }
}
