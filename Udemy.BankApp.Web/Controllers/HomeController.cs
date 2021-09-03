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
    public class HomeController : Controller
    {
        private readonly BankContext _bankContext;
        private readonly ApplicationUserRepository applicationUserRepository;

        public HomeController(BankContext bankContext)
        {
            _bankContext = bankContext;
            applicationUserRepository = new ApplicationUserRepository(_bankContext);
        }

        public IActionResult Index()
        {
            return View(applicationUserRepository.GetAll());
        }

        public IActionResult Index2()
        {
            return View(applicationUserRepository.GetAll());
        }
    }
}
