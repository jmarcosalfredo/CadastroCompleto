using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi;

namespace CadastroCompleto.Config
{
    public static class SwaggerConfig
    {
        private static readonly string AppName = "Cadastro Completo de Pessoas com Tabelas Relacionais";
        private static readonly string AppDescription = "API para cadastro completo de pessoas com tabelas relacionais, incluindo informações pessoais, endereço e contatos.";

        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppName,
                    Version = "v1",
                    Description = AppDescription
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", AppName);
                c.RoutePrefix = "swagger";
                c.DocumentTitle = AppName;
            });

            return app;
        }
    }
}
