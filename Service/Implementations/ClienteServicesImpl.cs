using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
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

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            await _clienteRepository.CreateAsync(cliente);
            return cliente;
        }

        public async Task<List<Cliente>> FindAllAsync()
        {
            return await _clienteRepository.FindAllAsync();
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            return await _clienteRepository.FindByIdAsync(id);
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            var clienteExistente = await _clienteRepository.FindByIdAsync(cliente.ClienteId);
            if (clienteExistente == null) return null;

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

            return cliente;
        }

        public async Task DeleteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }
    }
}
