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

        public Cliente Create(Cliente cliente)
        {
            _clienteRepository.Create(cliente);
            return cliente;
        }

        public List<Cliente> FindAll()
        {
            return _clienteRepository.FindAll();
        }

        public Cliente FindById(int id)
        {
            return _clienteRepository.FindById(id);
        }

        public Cliente Update(Cliente cliente)
        {
            var clienteExistente = _clienteRepository.FindById(cliente.ClienteId);
            if (clienteExistente == null) return null;

            _clienteRepository.Update(cliente);

            if (cliente.Endereco != null)
            {
                cliente.Endereco.ClienteId = cliente.ClienteId;
                _enderecoRepository.Update(cliente.Endereco);
            }

            var telefonesExistentes = clienteExistente.Telefones.ToList();
            var idsRecebidos = cliente.Telefones.Select(t => t.TelefoneId).ToHashSet();

            foreach (var telefone in cliente.Telefones)
            {
                telefone.ClienteId = cliente.ClienteId;

                if (telefone.TelefoneId == 0)
                    _telefoneRepository.Create(telefone);
                else
                    _telefoneRepository.Update(telefone);
            }

            var telefonesRemovidos = telefonesExistentes
                .Where(t => !idsRecebidos.Contains(t.TelefoneId));

            foreach (var telefone in telefonesRemovidos)
                _telefoneRepository.Delete(telefone.TelefoneId);

            return cliente;
        }

        public void Delete(int id)
        {
            _clienteRepository.Delete(id);
        }
    }
}
