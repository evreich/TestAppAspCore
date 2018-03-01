using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;

namespace TestAppAspCore.Controllers
{
    public class BookController : Controller
    {
        IBooksRepository _booksRepository;
        IGenresRepository _genresRepository;

        public BookController(IBooksRepository booksRepository, IGenresRepository genresRepository)
        {
            _booksRepository = booksRepository;
            _genresRepository = genresRepository;
        }

        // GET: Book/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(_genresRepository.GetAllGenres());
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book)
        {
            _booksRepository.AddBook(book);
            return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller",""));
        }

        // GET: Book/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _booksRepository.GetBook(id);
            ViewBag.Book = book;
            return View(_genresRepository.GetAllGenres());
        }

        // POST: Book/Edit/
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            _booksRepository.EditBook(book);
            return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller", ""));
        }

        // GET: Book/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = _booksRepository.GetBook(id);
            ViewBag.Book = book;
            return View(_genresRepository.GetAllGenres());
        }

        // POST: Book/Delete/
        [HttpPost]
        public ActionResult Delete(Book book)
        {
            _booksRepository.DeleteBook(book);
            return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller", ""));
        }
    }
}