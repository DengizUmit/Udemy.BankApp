using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankContext _bankContext;
        private readonly ApplicationUserRepository applicationUserRepository;
        public AccountController(BankContext bankContext)
        {
            _bankContext = bankContext;
            applicationUserRepository = new ApplicationUserRepository(_bankContext);
        }

        public IActionResult Create(int id)
        {
            var userInfo = applicationUserRepository.GetById(id);

            return View(userInfo);
        }

        [HttpPost]
        public IActionResult Create(AccountCreateModel accountCreateModel)
        {
            _bankContext.Accounts.Add(new Data.Entities.Account
            {
                AccountNumber = accountCreateModel.AccountNumber,
                ApplicationUserId = accountCreateModel.ApplicationUserId,
                Balance = accountCreateModel.Balance
            });
            _bankContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
