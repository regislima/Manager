using Manager.Core.Extensions;
using Manager.Domain.entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private IConfiguration _configuration;

        // Entity Framework Core
        public ManagerContext() { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                switch (_configuration["Configs:Database"])
                {
                    case "mysql":
                    case "mariadb":
                        optionsBuilder.UseMySql(ServerVersion.AutoDetect(_configuration.GetConnectionString("MySqlConnection")));
                        break;

                    case "sqlserver":
                        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServerConnection"));
                        break;
                    
                    default:
                        optionsBuilder.UseInMemoryDatabase(_configuration.GetConnectionString("InMemoryConnection"));
                        break;
                }
            }
        }
    }
}