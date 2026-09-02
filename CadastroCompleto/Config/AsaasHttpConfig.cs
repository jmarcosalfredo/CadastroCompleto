using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Service;
using CadastroCompleto.Service.Implementations;
using Microsoft.Extensions.Http.Resilience;
using Polly;

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
            })
            .AddResilienceHandler("asaas-resilience-policy", configuration =>
            {
                configuration.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    BackoffType = DelayBackoffType.Exponential, //tipo do tempo de espera a cada tentativa de retry, nesse caso dobra
                    UseJitter = true, //adiciona um tempo aleatório de espera em cada tentativa
                    Delay = TimeSpan.FromMilliseconds(500) // tempo de espera inicial antes da primeira tentativa de retry
                })
                .AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
                {
                    FailureRatio = 0.7, // porcentagem de falhas para abrir o circuito
                    SamplingDuration = TimeSpan.FromSeconds(30), // periodo de tempo para calcular a taxa de falhas
                    MinimumThroughput = 10, // numero minimo de requisições para considerar a taxa de falhas
                    BreakDuration = TimeSpan.FromSeconds(15) // tempo que o circuito ficará aberto antes de tentar fechar novamente
                })
                .AddTimeout(TimeSpan.FromSeconds(5)); // tempo máximo de espera para a resposta da requisição
            });

            services.AddScoped<IAsaasService, AsaasServiceImpl>();

            return services;
        }

    }
}
