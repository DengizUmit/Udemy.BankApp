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
    public class HomeController : Controller
    {
        private readonly BankContext _bankContext;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserMapper _userMapper;

        public HomeController(BankContext bankContext, IApplicationUserRepository applicationUserRepository, IUserMapper userMapper)
        {
            _bankContext = bankContext;
            _applicationUserRepository = applicationUserRepository;
            _userMapper = userMapper;
        }

        public IActionResult Index()
        {
            return View(_userMapper.MapToListOfUserList(_applicationUserRepository.GetAll()));
        }

        public IActionResult Index2()
        {
            return View(_userMapper.MapToListOfUserList(_applicationUserRepository.GetAll()));
        }
    }
}
