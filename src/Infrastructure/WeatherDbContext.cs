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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                // "Server=.;Database=WeatherService;Trusted_Connection=True; MultipleActiveResultSets=True");
                "Server = tcp:dnmalaga.database.windows.net, 1433; Initial Catalog = dnmalaga; Persist Security Info = False; User ID = dnmalaga; Password = Espeto2017; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
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
            var entity = modelBuilder.Entity<WeatherData>();
            entity.Property(e => e.Id).UseSqlServerIdentityColumn()
                .IsRequired();

            entity.Property(e => e.CityName)
                .IsRequired().HasMaxLength(50);
            entity.Property(e => e.MainWeather)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.WeatherDescription)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Temperature).IsRequired();
            entity.Property(e => e.Pressure).IsRequired();
            entity.Property(e => e.Lon).IsRequired();
            entity.Property(e => e.Lat).IsRequired();
            entity.Property<DateTime>("LastUpdated").IsRequired();
        }
    }
}
