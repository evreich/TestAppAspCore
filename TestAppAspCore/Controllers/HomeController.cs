using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.EFCore;
using TestAppAspCore.ModelHelpers;
using TestAppAspCore.ViewModels;

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
        [HttpGet]
        public ActionResult ShowBooks()
        {
            return View(_repository.GetAllBooks().
                            Select(book => BookConverter.ConvertModelToViewModel(book)).
                            ToList());
        }

        [Route("Home/ServerError")]
        public ActionResult ServerError()
        {
            return View();
        }

        [Route("Home/ErrorStatusCode")]
        public ActionResult ErrorStatusCode(int id)
        {
            ViewBag.ErrorCode = id;
            return View();
        }
    }
}