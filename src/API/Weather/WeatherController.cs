using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIHostedService.API.Weather
{
    [Route("api/weather")]
    public class WeatherController: Controller
    {
        private readonly WeatherDbContext _weatherDbContext;

        public WeatherController(WeatherDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var weatherData = await _weatherDbContext.WeatherData.
                OrderByDescending(w => w.Id).ToListAsync();
            return Ok(weatherData);
        }
    }
}
