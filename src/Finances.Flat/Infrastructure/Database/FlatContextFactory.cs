using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Finances.Flat.Infrastructure.Database
{
    internal class FlatContextFactory : IDbContextFactory<FlatContext>
    {
        public FlatContext Create(DbContextFactoryOptions options)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{options.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<FlatContext>()
                .UseSqlite(config.GetConnectionString("FlatDbConnection"));

            return new FlatContext(optionsBuilder.Options);
        }
    }
}
