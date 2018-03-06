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
        IBooksRepository _booksRepository;
        IGenresRepository _genresRepository;

        public HomeController(IBooksRepository booksRepository, IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
            _booksRepository = booksRepository;
        }

        // GET: Home/ShowBooks
        [HttpGet]
        public async Task<IActionResult> ShowBooks()
        {
            var books = await _booksRepository.GetAllBooks();
            return View(books.Select(book => BookConverter.ConvertModelToViewModel(book)).ToList());
        }

        // GET: Home/ShowGenres
        [HttpGet]
        public IActionResult ShowGenres()
        {
            return View(_genresRepository.GetAllGenres().ToList());
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