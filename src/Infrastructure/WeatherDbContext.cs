using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public DbSet<MaxMeasure> MaxMeasures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=WeatherService;Trusted_Connection=True; MultipleActiveResultSets=True");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var modified in modifiedEntries)
            {
                modified.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var weatherData = modelBuilder.Entity<WeatherData>();
            weatherData.Property(e => e.Id).UseSqlServerIdentityColumn()
                .IsRequired();

            weatherData.Property(e => e.CityName)
                .IsRequired().HasMaxLength(50);
            weatherData.Property(e => e.MainWeather)
                .IsRequired()
                .HasMaxLength(50);

            weatherData.Property(e => e.WeatherDescription)
                .IsRequired()
                .HasMaxLength(50);

            weatherData.Property(e => e.Temperature).IsRequired();
            weatherData.Property(e => e.Pressure).IsRequired();
            weatherData.Property(e => e.Lon).IsRequired();
            weatherData.Property(e => e.Lat).IsRequired();
            weatherData.Property<DateTime>("LastUpdated").IsRequired();

            var maxMeasure = modelBuilder.Entity<MaxMeasure>();
            maxMeasure.ToTable("MaxMeasures");
            maxMeasure.Property(m => m.Id).IsRequired().UseSqlServerIdentityColumn();
            maxMeasure.Property(m => m.CityName).HasMaxLength(50).IsRequired();
            maxMeasure.Property(m => m.Humidity).IsRequired();
            maxMeasure.Property(m => m.Pressure).IsRequired();
            maxMeasure.Property(m => m.Temperature).IsRequired();
            maxMeasure.Property<DateTime>("LastUpdated").IsRequired();
        }
    }
}
