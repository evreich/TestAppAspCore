using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.EFCore;

namespace TestAppAspCore.Controllers
{
    public class HomeController : Controller
    {
        IBooksRepository _repository;

        public HomeController(IBooksRepository repository)
        {
            _repository = repository;
        }

        // GET: Home/ShowBooks
        public ActionResult ShowBooks()
        {
            return View(_repository.GetAllBooks());
        }
    }
}