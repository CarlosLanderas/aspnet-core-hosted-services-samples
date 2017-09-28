using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIHostedService.Infrastructure.BackgroundTasks
{
    public class WeatherDataProcessor
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger _iLogger;

        public WeatherDataProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<WeatherDataProcessor> iLogger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            _iLogger = iLogger;
        }

        public async Task Execute()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<WeatherDbContext>();

                var maxTempCity = await dbContext.WeatherData
                    .FirstOrDefaultAsync(w => w.Temperature == dbContext.WeatherData.Max(wm => wm.Temperature));

                var maxHumidityCity = await dbContext.WeatherData
                    .FirstOrDefaultAsync(w => w.Humidity == dbContext.WeatherData.Max(w2 => w2.Humidity));

                var maxPressionCity = await dbContext.WeatherData
                    .FirstOrDefaultAsync(w => w.Pressure == dbContext.WeatherData.Max(w2 => w2.Pressure));

                var maxMeasureCities = new[] {maxTempCity, maxPressionCity, maxHumidityCity};
                foreach (var maxMeasureCity in maxMeasureCities)
                {
                    dbContext.MaxMeasures.Add(new MaxMeasure()
                    {
                        Humidity = maxMeasureCity.Humidity,
                        Temperature = maxMeasureCity.Temperature,
                        Pressure = maxMeasureCity.Pressure,
                        CityName = maxMeasureCity.CityName,
                        MeasureTime = maxMeasureCity.CreatedAt
                    });

                    await dbContext.SaveChangesAsync();
                    _iLogger.LogInformation("Weather measurement background task finished");
                }
                
            }

        }
    }
}
