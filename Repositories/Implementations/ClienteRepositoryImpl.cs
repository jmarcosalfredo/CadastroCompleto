using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using CadastroCompleto.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Repositories.Implementations
{
    public class ClienteRepositoryImpl : GenericRepositoryImpl<Cliente>, IClienteRepository
    {
        public ClienteRepositoryImpl(AppDbContext context) : base(context)
        {
        }

        override public List<Cliente> FindAll()
        {
            return _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Telefones.OrderBy(t => t.TelefoneId).Take(1))
                .ToList();
        }

        override public Cliente FindById(int id)
        {
            return _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Telefones)
                .FirstOrDefault(c => c.ClienteId == id);
        }
    }
}
