using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using CadastroCompleto.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Repositories.Implementations
{
    public class OutboxRepositoryImpl : GenericRepositoryImpl<Outbox>, IOutboxRepository
    {
        public OutboxRepositoryImpl(AppDbContext context) : base(context)
        {

        }
        public async Task<ICollection<Outbox>> FindPendentesAsync(int limite = 20)
        {
            return await _context.Outboxes
                .Include(e => e.Cliente)
                    .ThenInclude(c => c.Endereco)
                .Where(e => e.ProcessadoEm == null && e.Tentativas < 5)
                .OrderBy(e => e.CriadoEm)
                .Take(limite)
                .ToListAsync();
        }
    }
}
