using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIHostedService.Infrastructure.HostedServices
{
    public class WeatherHostedService : IHostedService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger _iLogger;
        private WeatherDbContext weatherContext;
        private const int delayMilliseconds = 15000;

        public WeatherHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<WeatherHostedService> iLogger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            _iLogger = iLogger;
        }
        private readonly HashSet<string> cities = new HashSet<string>()
        {
            "Madrid", "London", "Dubai",
            "New York", "Vancouver","Seattle",
            "Oregon","Galicia", "Miami", "Berlin"
        };
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    weatherContext = scope.ServiceProvider.GetService<WeatherDbContext>();

                    ICollection<Task<WeatherData>> cityTasks = new List<Task<WeatherData>>();
                    foreach (var city in cities)
                    {
                        cityTasks.Add(new WeatherClient().GetCity(city));
                    }

                    var taskResult = await Task.WhenAll(cityTasks.AsEnumerable());
                    foreach (var cityWeatherData in taskResult)
                    {
                        weatherContext.WeatherData.Add(cityWeatherData);
                    }

                    await weatherContext.SaveChangesAsync(cancellationToken);
                    _iLogger.LogInformation($"Downloaded weather data for cities ${string.Join(",", cities)} ");
                    await Task.Delay(delayMilliseconds,cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
