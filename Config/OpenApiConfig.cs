using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.OpenApi;

namespace CadastroCompleto.Config
{
    public static class OpenApiConfig
    {
        private static readonly string AppName = "Cadastro Completo de Pessoas com Tabelas Relacionais";
        private static readonly string AppDescription = "API para cadastro completo de pessoas com tabelas relacionais, incluindo informações pessoais, endereço e contatos.";

        public static IServiceCollection AddOpenApiConfig(this IServiceCollection services)
        {
            services.AddSingleton(new OpenApiInfo
            {
                Title = AppName,
                Version = "v1",
                Description = AppDescription
            });

            return services;
        }
    }
}
