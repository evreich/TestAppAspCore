using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.EFCore;
using TestAppAspCore.ModelHelpers;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Controllers
{
    public class HomeController : Controller
    {
        const int countElemOnPage = 5;

        IBooksRepository _booksRepository;
        IGenresRepository _genresRepository;

        public HomeController(IBooksRepository booksRepository, IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
            _booksRepository = booksRepository;
        }

        // GET: Home/ShowBooks
        [HttpGet]
        public async Task<IActionResult> ShowBooks(string searchExpr, int pageNum)
        {
            IEnumerable<Book> books = new List<Book>();
            if(string.IsNullOrEmpty(searchExpr))
            {
                books = await _booksRepository.GetAllBooks();
            }
            else
            {
                books = await _booksRepository.GetBooksByFilter(searchExpr, _genresRepository.GetAllGenres().ToList());
            }
            var indexViewModel = new IndexBookViewModel
            {
                Books = books.Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                PageViewModel = new PageViewModel(books.Count(), pageNum, countElemOnPage)
            };
            return View(indexViewModel);
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