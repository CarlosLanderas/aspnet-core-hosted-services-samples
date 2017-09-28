using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIHostedService.Controllers
{
    [Route("api/maxmeasures")]
    public class MaxMeasuresController: Controller
    {
        private readonly WeatherDbContext _weatherContext;

        public MaxMeasuresController(WeatherDbContext weatherContext)
        {
            _weatherContext = weatherContext;
        }

        [HttpGet, Route("temp")]
        public async Task<IActionResult> GetMaxTemp()
        {
            var maxTemp = await _weatherContext.MaxMeasures
                .OrderByDescending(m => m.Temperature).FirstOrDefaultAsync();
            return Ok(maxTemp);
;        }

        [HttpGet, Route("pressure")]
        public async Task<IActionResult> GetMaxPressure()
        {
            var maxTemp = await _weatherContext.MaxMeasures
                .OrderByDescending(m => m.Temperature).FirstOrDefaultAsync();
            return Ok(maxTemp);
            ;
        }

        [HttpGet, Route("humidity")]
        public async Task<IActionResult> GetMaxHumidity()
        {
            var maxTemp = await _weatherContext.MaxMeasures
                .OrderByDescending(m => m.Humidity).FirstOrDefaultAsync();
            return Ok(maxTemp);
            ;
        }

    }
}
