using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.Repositories;

namespace Udemy.BankApp.Web.Data.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly BankContext _bankContext;

        public Uow(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_bankContext);
        }

        public void SaveChanges()
        {
            _bankContext.SaveChanges();
        }
    }
}
