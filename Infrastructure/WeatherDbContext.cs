using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIHostedService.Model;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIHostedService.Infrastructure
{
    public class WeatherDbContext: DbContext
    {
        public WeatherDbContext(DbContextOptions options): base(options){}
        public WeatherDbContext() { }
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=WeatherService;Trusted_Connection=True; MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherData>().HasKey(w => w.CityName);
        }
    }
}
