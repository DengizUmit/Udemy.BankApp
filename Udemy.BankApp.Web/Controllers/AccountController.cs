using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            ViewBag.FullName = user.Name + " " + user.Surname;

            var list = new List<AccountListModel>();

            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _accountRepository.GetQueryable();
            var accounts = query.Where(x => x.Id != accountId).ToList();
            var list = new List<AccountListModel>();

            ViewBag.SenderId = accountId;

            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }

            return View(new SelectList(list, "Id", "AccountNumber"));
        }

        [HttpPost]
        public IActionResult SendMoney(SendMoneyModel model)
        {
            var senderAccount = _accountRepository.GetById(model.SenderId);
            senderAccount.Balance -= model.Amount;
            _accountRepository.Update(senderAccount);

            var account = _accountRepository.GetById(model.AccountId);
            account.Balance += model.Amount;
            _accountRepository.Update(account);

            return RedirectToAction("Index", "Home");
        }
    }
}
