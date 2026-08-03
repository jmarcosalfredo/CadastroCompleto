using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;

namespace CadastroCompleto.Repositories.Implementations
{
    public interface IClienteRepository
    {
        Cliente Create(Cliente cliente);
        Cliente FindById(int id);
        List<Cliente> FindAll();
        Cliente Update(Cliente cliente);
        void Delete(int id);
    }
}
