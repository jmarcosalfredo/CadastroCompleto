using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Data;
using CadastroCompleto.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Repositories.Implementations
{
    public class GenericRepositoryImpl<T> : IRepository<T> where T : BaseEntity
    {
        protected AppDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepositoryImpl(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task DeleteAsync(int id)
        {
            {
                var existente = await _dbSet.FindAsync(id);
                if (existente == null)
                    return;

                _dbSet.Remove(existente);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<List<T>> FindAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var pk = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey();
            var keyValues = pk.Properties.Select(p => p.PropertyInfo.GetValue(entity)).ToArray();

            var existente = await _dbSet.FindAsync(keyValues);
            if (existente == null)
                return null;

            _context.Entry(existente).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
