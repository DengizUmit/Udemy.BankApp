using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Mapping;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IAccountRepository _accountRepository;
        //private readonly IAccountMapper _accountMapper;
        //private readonly IUserMapper _userMapper;

        //public AccountController(IApplicationUserRepository applicationUserRepository, IUserMapper userMapper, IAccountRepository accountRepository, IAccountMapper accountMapper)
        //{
        //    _applicationUserRepository = applicationUserRepository;
        //    _userMapper = userMapper;
        //    _accountRepository = accountRepository;
        //    _accountMapper = accountMapper;
        //}

        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<Account> _accountRepository;

        public AccountController(IRepository<Account> accountRepository, IRepository<ApplicationUser> userRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        //public IActionResult Create(int id)
        //{
        //    var userInfo = _userRepository.GetById(id);
        //    return View(new UserListModel
        //    {
        //        Id = userInfo.Id,
        //        Name = userInfo.Name,
        //        Surname = userInfo.Surname
        //    });
        //}

        [HttpPost]
        public IActionResult Create(AccountCreateModel accountCreateModel)
        {
            _accountRepository.Create(new Account
            {
                AccountNumber = accountCreateModel.AccountNumber,
                ApplicationUserId = accountCreateModel.ApplicationUserId,
                Balance = accountCreateModel.Balance
            });
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _accountRepository.GetQueryable();
            var accounts = query.Where(x => x.ApplicationUserId == userId).ToList();

            var user = _userRepository.GetById(userId);
            var list = new List<AccountListModel>();

            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    FullName = user.Name+ " " +user.Surname,
                    Id = account.Id
                });
            }

            return View(list);
        }
    }
}
