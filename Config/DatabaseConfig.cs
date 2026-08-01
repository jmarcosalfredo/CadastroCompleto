using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Config
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfigurarion(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:PostgresConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ConnectionStrings:PostgresConnection' not found in configuration.");
            }

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

            return services;
        }
    }
}
