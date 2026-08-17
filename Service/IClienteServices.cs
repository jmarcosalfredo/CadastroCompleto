using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;

namespace CadastroCompleto.Service
{
    public interface IClienteServices
    {
        Task<Cliente> CreateAsync(Cliente cliente);
        Task<Cliente> FindByIdAsync(int id);
        Task<List<Cliente>> FindAllAsync();
        Task<Cliente> UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}
