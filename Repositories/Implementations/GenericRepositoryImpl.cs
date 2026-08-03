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
        private AppDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepositoryImpl(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Create(T entity)
        {
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }

        public void Delete(int id)
        {
            {
                var existente = _dbSet.Find(id);
                if (existente == null) return;

                _dbSet.Remove(existente);
                _context.SaveChanges();
            }
        }

        public List<T> FindAll()
        {
            return _dbSet.ToList();
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Update(T entity)
        {
            var pk = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey();
            var keyValues = pk.Properties.Select(p => p.PropertyInfo.GetValue(entity)).ToArray();

            var existente = _dbSet.Find(keyValues);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
