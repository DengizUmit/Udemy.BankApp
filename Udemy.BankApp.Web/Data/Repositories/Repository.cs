using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class , new()
    {
        private readonly BankContext _bankContext;
        public void Create(T entity)
        {
            _bankContext.Set<T>().Add(entity);
            _bankContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            _bankContext.Set<T>().Remove(entity);
            _bankContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _bankContext.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _bankContext.Set<T>().Update(entity);
            _bankContext.SaveChanges();
        }

        public IQueryable<T> GetQueryable()
        {
            return _bankContext.Set<T>().AsQueryable();
        }
    }
}
