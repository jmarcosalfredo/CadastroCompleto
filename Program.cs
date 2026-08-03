using CadastroCompleto.Config;
using CadastroCompleto.Data;
using CadastroCompleto.Repositories.Implementations;
using CadastroCompleto.Service;
using CadastroCompleto.Service.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiConfig();
builder.Services.AddSwaggerConfig();

builder.Services.AddDatabaseConfigurarion(builder.Configuration);

builder.Services.AddScoped<IClienteServices, ClienteServicesImpl>();
builder.Services.AddScoped<IClienteRepository, ClienteRepositoryImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwaggerConfig();

app.Run();
