using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Base;

namespace CadastroCompleto.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> FindAllAsync();
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
