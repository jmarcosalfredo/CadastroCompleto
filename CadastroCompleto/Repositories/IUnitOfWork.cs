using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Repositories.Implementations;

namespace CadastroCompleto.Repositories
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }
        IRepository<Endereco> EnderecoRepository { get; }
        IRepository<Telefone> TelefoneRepository { get; }

        Task<int> CommitAsync();
    }
}
