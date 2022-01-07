using System;
using Manager.Core.Extensions;
using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        private readonly IConfiguration _configuration;

        // Entity Framework Core
        public ManagerContext() { }

        public ManagerContext(DbContextOptions<ManagerContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration.IsNull())
                throw new Exception("Arquivo de configuração não encontrado");
            else
            {
                switch (_configuration["Configs:Provider"])
                {
                    case "mysql":
                    case "mariadb":
                        optionsBuilder.UseMySql(
                            @"Server=172.17.0.1;Port=3306;Database=managerapi;Uid=regis;Pwd=root",
                            new MySqlServerVersion(new Version(10, 6, 4))
                        );
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