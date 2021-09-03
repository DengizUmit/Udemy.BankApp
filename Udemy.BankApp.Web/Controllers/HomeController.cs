using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BankContext _bankContext;

        public HomeController(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public IActionResult Index()
        {
            return View(_bankContext.ApplicationUsers.Select(x => new UserListModel
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname
            }).ToList());
        }
    }
}
