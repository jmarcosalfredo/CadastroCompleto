using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.Responses;

namespace CadastroCompleto.Service
{
    public interface IClienteServices
    {
        Task<ServiceResponse<Cliente>> CreateAsync(Cliente cliente);
        Task<ServiceResponse<Cliente>> FindByIdAsync(int id);
        Task<ServiceResponse<List<Cliente>>> FindAllAsync();
        Task<ServiceResponse<Cliente>> UpdateAsync(Cliente cliente);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
