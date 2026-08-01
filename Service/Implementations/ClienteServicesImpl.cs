using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using CadastroCompleto.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Service.Implementations
{
    public class ClienteServicesImpl : IClienteServices
    {
        private AppDbContext _context;

        public ClienteServicesImpl(AppDbContext context)
        {
            _context = context;
        }

        public Cliente Create(Cliente cliente)
        {
            _context.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public List<Cliente> FindAll()
        {
            return _context.Clientes.Include(c => c.Endereco).ToList();
        }

        public Cliente FindById(int id)
        {
            return _context.Clientes.Find(id);
        }

        public Cliente Update(Cliente cliente)
        {
            var clienteExistente = _context.Clientes.Find(cliente.ClienteId);

            if (clienteExistente == null)
            {
                return null;
            }

            _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);
            _context.SaveChanges();
            return cliente;
        }

        public void Delete(int id)
        {
            var clienteExistente = _context.Clientes.Find(id);

            if (clienteExistente == null)
            {
                return;
            }

            _context.Clientes.Remove(clienteExistente);
            _context.SaveChanges();
        }
    }
}
