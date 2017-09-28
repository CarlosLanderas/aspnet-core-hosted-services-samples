using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Infrastructure;
using AspNetCoreIHostedService.Infrastructure.Authorization;
using AspNetCoreIHostedService.Infrastructure.BackgroundTasks;
using AspNetCoreIHostedService.Infrastructure.HostedServices;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreIHostedService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<WeatherDbContext>();

            var serviceProvider = services.BuildServiceProvider();
            var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            services.AddSingleton<IHostedService, WeatherHostedService>();

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });

            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHangfireServer();
            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = new[] { new DashboardAuthentication() }
            });

            RecurringJob.AddOrUpdate<WeatherDataProcessor>(w => w.Execute(), "0/15 * * * *");

            app.UseMvc();
        }
    }
}
