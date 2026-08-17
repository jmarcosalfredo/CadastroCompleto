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

        override public async Task<List<Cliente>> FindAllAsync()
        {
            return await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Telefones.OrderBy(t => t.TelefoneId).Take(1))
                .ToListAsync();
        }

        override public async Task<Cliente> FindByIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }
    }
}
