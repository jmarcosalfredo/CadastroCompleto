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
        private IClienteRepository _clienteRepository;
        private IRepository<Endereco> _enderecoRepository;
        private IRepository<Telefone> _telefoneRepository;

        public ClienteServicesImpl(IClienteRepository clienteRepository, IRepository<Endereco> enderecoRepository, IRepository<Telefone> telefoneRepository)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<ServiceResponse<Cliente>> CreateAsync(Cliente cliente)
        {
            try
            {
                var result = await _clienteRepository.CreateAsync(cliente);
                return ServiceResponse<Cliente>.ComSucesso(result, "Cliente criado com sucesso!");
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
                var result = await _clienteRepository.FindAllAsync();
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
                var cliente = await _clienteRepository.FindByIdAsync(id);

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
                var clienteExistente = await _clienteRepository.FindByIdAsync(cliente.ClienteId);

                if (clienteExistente == null)
                    return ServiceResponse<Cliente>.ComFalha("Cliente não encontrado!");

                await _clienteRepository.UpdateAsync(cliente);

                if (cliente.Endereco != null)
                {
                    cliente.Endereco.ClienteId = cliente.ClienteId;
                    await _enderecoRepository.UpdateAsync(cliente.Endereco);
                }

                var telefonesExistentes = clienteExistente.Telefones.ToList();
                var idsRecebidos = cliente.Telefones.Select(t => t.TelefoneId).ToHashSet();

                foreach (var telefone in cliente.Telefones)
                {
                    telefone.ClienteId = cliente.ClienteId;

                    if (telefone.TelefoneId == 0)
                        await _telefoneRepository.CreateAsync(telefone);
                    else
                        await _telefoneRepository.UpdateAsync(telefone);
                }

                var telefonesRemovidos = telefonesExistentes
                    .Where(t => !idsRecebidos.Contains(t.TelefoneId));

                foreach (var telefone in telefonesRemovidos)
                    await _telefoneRepository.DeleteAsync(telefone.TelefoneId);


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
                var response = await _clienteRepository.FindByIdAsync(id);

                if (response == null)
                {
                    return ServiceResponse<bool>.ComFalha("Cliente não encontrado!");
                }

                await _clienteRepository.DeleteAsync(id);
                return ServiceResponse<bool>.ComSucesso(true, "Cliente deletado!");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ComFalha(ex.Message);
            }
        }
    }
}
