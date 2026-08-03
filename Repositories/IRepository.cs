using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Base;

namespace CadastroCompleto.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T FindById(int id);
        List<T> FindAll();
        T Update(T entity);
        void Delete(int id);
    }
}
