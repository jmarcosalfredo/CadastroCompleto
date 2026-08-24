using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using CadastroCompleto.Models;

namespace CadastroCompleto.Repositories.Implementations
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        private IClienteRepository _clienteRepo;

        private IRepository<Endereco> _enderecoRepo;

        private IRepository<Telefone> _telefoneRepo;

        public AppDbContext _context;

        public UnitOfWorkImpl(AppDbContext context)
        {
            _context = context;
        }

        public IClienteRepository ClienteRepository
        {
            get
            {
                if (_clienteRepo == null)
                {
                    _clienteRepo = new ClienteRepositoryImpl(_context);
                }
                return _clienteRepo;
            }
        }

        public IRepository<Endereco> EnderecoRepository
        {
            get
            {
                if (_enderecoRepo == null)
                {
                    _enderecoRepo = new GenericRepositoryImpl<Endereco>(_context);
                }
                return _enderecoRepo;
            }
        }

        public IRepository<Telefone> TelefoneRepository
        {
            get
            {
                if (_telefoneRepo == null)
                {
                    _telefoneRepo = new GenericRepositoryImpl<Telefone>(_context);
                }
                return _telefoneRepo;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
