using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;

namespace CadastroCompleto.Repositories
{
    public interface IOutboxRepository : IRepository<Outbox>
    {
        Task<ICollection<Outbox>> FindPendentesAsync(int limite = 20);
    }
}
