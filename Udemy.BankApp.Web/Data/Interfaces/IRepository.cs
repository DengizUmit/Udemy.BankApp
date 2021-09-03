using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy.BankApp.Web.Data.Interfaces
{
    public interface IRepository<T> where T : class, new()
    {
        void Create(T entity);
        void Remove(T entity);
        List<T> GetAll();
        void Update(T entity);
    }
}
