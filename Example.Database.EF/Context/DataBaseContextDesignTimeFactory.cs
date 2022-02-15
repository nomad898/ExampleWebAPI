using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Example.Database.EF.Context.Configurations;

namespace Example.Database.EF.Context
{
    public class DataBaseContextDesignTimeFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public DatabaseContext CreateDbContext(string[] args)
        {
            string connectionStringName = "ExampleCS";
            string connectionString = Configuration.GetConnectionString(connectionStringName);
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
