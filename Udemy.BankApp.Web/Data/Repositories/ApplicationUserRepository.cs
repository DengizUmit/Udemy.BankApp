using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly BankContext _bankContext;

        public ApplicationUserRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public List<ApplicationUser> GetAll()
        {
            return _bankContext.ApplicationUsers.ToList();
        }

        public ApplicationUser GetById(int id)
        {
            return _bankContext.ApplicationUsers.SingleOrDefault(x => x.Id == id);
        }

        public void Create(ApplicationUser applicationUser)
        {
            _bankContext.ApplicationUsers.Add(applicationUser);
            _bankContext.SaveChanges();
        }
    }
}
