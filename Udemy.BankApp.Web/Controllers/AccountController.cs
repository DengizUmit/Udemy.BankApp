using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Mapping;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankContext _bankContext;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserMapper _userMapper;

        public AccountController(BankContext bankContext, IApplicationUserRepository applicationUserRepository, IUserMapper userMapper)
        {
            _bankContext = bankContext;
            _applicationUserRepository = applicationUserRepository;
            _userMapper = userMapper;
        }

        public IActionResult Create(int id)
        {
            var userInfo = _userMapper.MapToUserList(_applicationUserRepository.GetById(id));

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
