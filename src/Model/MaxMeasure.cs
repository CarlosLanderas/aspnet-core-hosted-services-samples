using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIHostedService.Model
{
    public class MaxMeasure
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public DateTime MeasureTime { get; set; }
    }
}
