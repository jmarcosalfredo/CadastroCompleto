using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using CadastroCompleto.Models;
using CadastroCompleto.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Service.Implementations
{
    public class ClienteServicesImpl : IClienteServices
    {
        private IClienteRepository _clienteRepository;

        public ClienteServicesImpl(IClienteRepository context)
        {
            _clienteRepository = context;
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
            _clienteRepository.Update(cliente);
            return cliente;
        }

        public void Delete(int id)
        {
            _clienteRepository.Delete(id);
        }
    }
}
