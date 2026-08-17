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
            var response = new ServiceResponse<Cliente>();

            try
            {
                await _clienteRepository.CreateAsync(cliente);
                response.Dados = cliente;
                response.Mensagem = "Cliente criado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }

            return response;

        }

        public async Task<ServiceResponse<List<Cliente>>> FindAllAsync()
        {
            var response = new ServiceResponse<List<Cliente>>();

            try
            {
                response.Dados = await _clienteRepository.FindAllAsync();
                response.Mensagem = "Lista encontrada com sucesso!";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Cliente>> FindByIdAsync(int id)
        {
            var response = new ServiceResponse<Cliente>();

            try
            {
                var cliente = await _clienteRepository.FindByIdAsync(id);

                if (cliente == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Cliente não existe no banco de dados!";
                    return response;
                }

                response.Dados = cliente;
                response.Mensagem = "Cliente encontrado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Cliente>> UpdateAsync(Cliente cliente)
        {
            var response = new ServiceResponse<Cliente>();
            try
            {
                var clienteExistente = await _clienteRepository.FindByIdAsync(cliente.ClienteId);
                if (clienteExistente == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Cliente não encontrado!";
                    return response;
                }


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


                response.Dados = cliente;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var clienteExistente = await _clienteRepository.FindByIdAsync(id);

                if (clienteExistente == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Cliente não encontrado!";
                    response.Dados = false;

                    return response;
                }

                await _clienteRepository.DeleteAsync(id);
                response.Dados = true;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = ex.Message;
            }

            return response;
        }
    }
}
