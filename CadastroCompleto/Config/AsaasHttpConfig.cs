using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Service;
using CadastroCompleto.Service.Implementations;

namespace CadastroCompleto.Config
{
    public static class AsaasHttpConfig
    {
        public const string ClientName = "Asaas";

        public static IServiceCollection AddAsaasConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["Asaas:BaseUrl"] ?? throw new ArgumentNullException("Asaas: Configure a URL BASE do Asaas no appsettings.json");
            var apiKey = configuration["Asaas:ApiKey"] ?? throw new ArgumentNullException("Asaas: Configure a API KEY do Asaas no appsettings.json");

            services.AddHttpClient(ClientName, client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("access_token", apiKey);
                client.DefaultRequestHeaders.Add("User-Agent", "CadastroCompleto");
            });

            services.AddScoped<IAsaasService, AsaasServiceImpl>();

            return services;
        }

    }
}
