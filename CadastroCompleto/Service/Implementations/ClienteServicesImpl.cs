using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.Responses;
using CadastroCompleto.Repositories;
using CadastroCompleto.Repositories.Implementations;

namespace CadastroCompleto.Service.Implementations
{
    public class ClienteServicesImpl : IClienteServices
    {
        private readonly IUnitOfWork _uof;
        private readonly IAsaasService _asaasService;

        public ClienteServicesImpl(IUnitOfWork uof, IAsaasService asaasService)
        {
            _uof = uof;
            _asaasService = asaasService;
        }

        public async Task<ServiceResponse<Cliente>> CreateAsync(Cliente cliente)
        {
            try
            {
                await _uof.ClienteRepository.CreateAsync(cliente);

                var outboxEvent = new Outbox
                {
                    Tipo = "RegistrarClienteAsaas",
                    Cliente = cliente,
                    CriadoEm = DateTimeOffset.UtcNow,
                };

                await _uof.OutboxRepository.CreateAsync(outboxEvent);
                await _uof.CommitAsync();

                return ServiceResponse<Cliente>.ComSucesso(cliente, "Cliente criado com sucesso!");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Cliente>.ComFalha(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<Cliente>>> FindAllAsync()
        {
            try
            {
                var result = await _uof.ClienteRepository.FindAllAsync();
                return ServiceResponse<List<Cliente>>.ComSucesso(result, "Lista encontrada com sucesso!");
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<Cliente>>.ComFalha(ex.Message);
            }
        }

        public async Task<ServiceResponse<Cliente>> FindByIdAsync(int id)
        {
            try
            {
                var cliente = await _uof.ClienteRepository.FindByIdAsync(id);

                if (cliente == null)
                    return ServiceResponse<Cliente>.ComFalha("Cliente não encontrado");

                return ServiceResponse<Cliente>.ComSucesso(cliente, "Cliente encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Cliente>.ComFalha(ex.Message);
            }
        }

        public async Task<ServiceResponse<Cliente>> UpdateAsync(Cliente cliente)
        {
            try
            {
                var clienteExistente = await _uof.ClienteRepository.FindByIdAsync(cliente.ClienteId);

                if (clienteExistente == null)
                    return ServiceResponse<Cliente>.ComFalha("Cliente não encontrado!");

                await _uof.ClienteRepository.UpdateAsync(cliente);
                await _uof.CommitAsync();

                if (cliente.Endereco != null)
                {
                    cliente.Endereco.ClienteId = cliente.ClienteId;
                    await _uof.EnderecoRepository.UpdateAsync(cliente.Endereco);
                    await _uof.CommitAsync();
                }

                var telefonesExistentes = clienteExistente.Telefones.ToList();
                var idsRecebidos = cliente.Telefones.Select(t => t.TelefoneId).ToHashSet();

                foreach (var telefone in cliente.Telefones)
                {
                    telefone.ClienteId = cliente.ClienteId;

                    if (telefone.TelefoneId == 0)
                    {
                        await _uof.TelefoneRepository.CreateAsync(telefone);
                        await _uof.CommitAsync();
                    }
                    else
                    {
                        await _uof.TelefoneRepository.UpdateAsync(telefone);
                        await _uof.CommitAsync();
                    }
                }

                var telefonesRemovidos = telefonesExistentes
                    .Where(t => !idsRecebidos.Contains(t.TelefoneId));

                foreach (var telefone in telefonesRemovidos)
                {
                    await _uof.TelefoneRepository.DeleteAsync(telefone.TelefoneId);
                    await _uof.CommitAsync();
                }


                return ServiceResponse<Cliente>.ComSucesso(cliente, "Dados alterados com sucesso!");
            }
            catch (Exception ex)
            {
                return ServiceResponse<Cliente>.ComFalha(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var response = await _uof.ClienteRepository.FindByIdAsync(id);

                if (response == null)
                {
                    return ServiceResponse<bool>.ComFalha("Cliente não encontrado!");
                }

                await _uof.ClienteRepository.DeleteAsync(id);
                await _uof.CommitAsync();
                return ServiceResponse<bool>.ComSucesso(true, "Cliente deletado!");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ComFalha(ex.Message);
            }
        }
    }
}
