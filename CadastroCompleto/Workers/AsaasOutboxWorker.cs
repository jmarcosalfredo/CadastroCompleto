using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Repositories;
using CadastroCompleto.Service;

namespace CadastroCompleto.Workers
{
    public class AsaasOutboxWorker : BackgroundService
    {
        private readonly Guid _instanciaId = Guid.NewGuid();
        private static readonly TimeSpan _intervaloExecucao = TimeSpan.FromSeconds(10);
        private readonly ILogger<AsaasOutboxWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public AsaasOutboxWorker(IServiceProvider serviceProvider, ILogger<AsaasOutboxWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado. Instância: {InstanciaId}", _instanciaId);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessarPendentesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar pendências da Outbox");
                }

                await Task.Delay(_intervaloExecucao, stoppingToken);
            }
        }

        private async Task ProcessarPendentesAsync(CancellationToken stoppingToken)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var uof = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var asaasService = serviceScope.ServiceProvider.GetRequiredService<IAsaasService>();

            var pendentes = await uof.OutboxRepository.FindPendentesAsync();

            foreach (var evento in pendentes)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    var respostaAsaas = await asaasService.CreateCustumerAsync(evento.Cliente);

                    evento.Cliente.AsaasNumber = respostaAsaas.Id;
                    await uof.ClienteRepository.UpdateAsync(evento.Cliente);

                    evento.ProcessadoEm = DateTimeOffset.UtcNow;
                    await uof.OutboxRepository.UpdateAsync(evento);

                    await uof.CommitAsync();
                }
                catch (Exception ex)
                {
                    evento.Tentativas++;
                    evento.UltimoErro = ex.Message;
                    await uof.OutboxRepository.UpdateAsync(evento);
                    await uof.CommitAsync();
                    _logger.LogWarning(ex, $"Erro ao processar evento da Outbox: {evento.OutboxId}, Tentativas: {evento.Tentativas}");
                }
            }
        }
    }
}
