using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.ModelHelpers;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TestAppAspCore.Controllers
{
    [Authorize(Roles = RolesConstants.ADMIN_ROLE)]
    public class BookController : Controller
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IGenresRepository _genresRepository;

        public BookController(IBooksRepository booksRepository, IGenresRepository genresRepository)
        {
            _booksRepository = booksRepository;
            _genresRepository = genresRepository;
        }

        // GET: Book/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ActionBooksPagesViewModel
            {
                Book = null,
                Genres = new SelectList(_genresRepository.GetAllGenres(), nameof(Genre.Id), nameof(Genre.Title)),
                PrevPageUrl = HttpContext.Request.Headers["Referer"].ToString()
            });
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel book, string prevPageUrl)
        {
            if (ModelState.IsValid)
            {
                _booksRepository.AddBook(BookConverter.ConvertViewModelToModel(book));
                TempData["message"] = $"Книга \"{book.Title}\" успешно добавлена";
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(new ActionBooksPagesViewModel
                {
                    Book = book,
                    Genres = new SelectList(_genresRepository.GetAllGenres(), nameof(Genre.Id), nameof(Genre.Title)),
                    PrevPageUrl = prevPageUrl
                });
            }
        }

        // GET: Book/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = BookConverter.ConvertModelToViewModel(_booksRepository.GetBook(id));

            return View(new ActionBooksPagesViewModel
            {
                Book = book,
                Genres = new SelectList(_genresRepository.GetAllGenres(), nameof(Genre.Id), nameof(Genre.Title),
                                        _genresRepository.GetAllGenres().FirstOrDefault(g => g.Id == book.GenreId)),
                PrevPageUrl = HttpContext.Request.Headers["Referer"].ToString()
            }); 
        }

        // POST: Book/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel book, string prevPageUrl)
        {
            if (book.Id <= 0)
                throw new ArgumentException("Book ID is empty or <= 0");
            if (ModelState.IsValid)
            {
                _booksRepository.EditBook(BookConverter.ConvertViewModelToModel(book));
                TempData["message"] = $"Книга \"{book.Title}\" успешно изменена";
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(new ActionBooksPagesViewModel
                {
                    Book = book,
                    Genres = new SelectList(_genresRepository.GetAllGenres(), nameof(Genre.Id), nameof(Genre.Title),
                                        _genresRepository.GetAllGenres().FirstOrDefault(g => g.Id == book.GenreId)),
                    PrevPageUrl = prevPageUrl
                });
            }
        }

        // GET: Book/Show/{id}
        [HttpGet]
        public ActionResult Show(int id)
        {
            var book = BookConverter.ConvertModelToViewModel(_booksRepository.GetBook(id));

            return View(book);
        }

        // GET: Book/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = BookConverter.ConvertModelToViewModel(_booksRepository.GetBook(id));

            return View(new ActionBooksPagesViewModel
            {
                Book = book,
                Genres = new SelectList(_genresRepository.GetAllGenres(), nameof(Genre.Id), nameof(Genre.Title),
                                        _genresRepository.GetAllGenres().FirstOrDefault(g => g.Id == book.GenreId)),
                PrevPageUrl = HttpContext.Request.Headers["Referer"].ToString()
            });
        }

        // POST: Book/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BookViewModel book, string prevPageUrl)
        {
            if (book.Id <= 0)
                throw new ArgumentException("Book ID is empty or <= 0");
            if (ModelState.IsValid)
            {
                _booksRepository.DeleteBook(BookConverter.ConvertViewModelToModel(book));
                TempData["message"] = $"Книга \"{book.Title}\" успешно удалена";
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(new ActionBooksPagesViewModel
                {
                    Book = book,
                    Genres = new SelectList(_genresRepository.GetAllGenres(), nameof(Genre.Id), nameof(Genre.Title),
                                        _genresRepository.GetAllGenres().FirstOrDefault(g => g.Id == book.GenreId)),
                    PrevPageUrl = prevPageUrl
                });
            }
        }
    }
}