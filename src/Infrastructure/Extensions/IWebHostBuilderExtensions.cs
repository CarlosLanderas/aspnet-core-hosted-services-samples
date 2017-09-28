using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIHostedService.Infrastructure.Extensions
{
    public static class IWebHostBuilderExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TContext>();

                try
                {
                    context.Database
                        .Migrate();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}
