using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIHostedService.Infrastructure.BackgroundTasks
{
    public class WeatherDataProcessor
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public WeatherDataProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public void Execute()
        {
           
        }
    }
}
