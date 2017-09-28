using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Infrastructure;
using AspNetCoreIHostedService.Infrastructure.BackgroundTasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIHostedService.API.Notification
{
    [Route("api/notification")]
    public class NotificationController: Controller
    {
        private readonly WeatherDbContext _weatherContext;

        public NotificationController(WeatherDbContext weatherContext)
        {
            _weatherContext = weatherContext;
        }
        [HttpPost, Route("last/measure")]
        public async Task<IActionResult> NotifyLastMeasure([FromBody] string email)
        {
            var measure = await _weatherContext.MaxMeasures.OrderByDescending(w => w.Id).FirstOrDefaultAsync();
            var mailBody = $"Last processed city is {measure.CityName} " +
                           $"with Temperature: {measure.Temperature}, " +
                           $"Pression: {measure.Pressure} and Humidity: {measure.Humidity}";

            BackgroundJob.Enqueue<SendGridNotifier>(s => s.Send(email, "Weather information", mailBody));
            return Ok(new{enqueued = true});
        }
    }
}
