using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.ModelHelpers;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;
using System.Linq;

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
            try
            {
                return View(new ActionBooksPagesViewModel
                {
                    Book = null,
                    Genres = new SelectList(_genresRepository.GetAllGenres(), "Id", "Title")
                });
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel book)
        {         
            try
            {
                if (ModelState.IsValid)
                {
                    _booksRepository.AddBook(BookConverter.ConvertViewModelToModel(book));
                    return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    return View(book);
                }
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: Book/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var book = BookConverter.ConvertModelToViewModel(_booksRepository.GetBook(id));

                return View(new ActionBooksPagesViewModel
                {
                    Book = book,
                    Genres = new SelectList(_genresRepository.GetAllGenres(), "Id", "Title",
                                            _genresRepository.GetAllGenres().FirstOrDefault(g => g.Id == book.GenreId))
                }); 
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Book/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel book)
        {
            try
            {
                if (book.Id <= 0)
                    throw new ArgumentException("Book ID is empty or <= 0");
                if (ModelState.IsValid)
                {
                    _booksRepository.EditBook(BookConverter.ConvertViewModelToModel(book));
                    return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    return View(book);
                }

            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Book/Show/{id}
        [HttpGet]
        public ActionResult Show(int id)
        {
            try
            {
                var book = BookConverter.ConvertModelToViewModel(_booksRepository.GetBook(id));

                return View(book);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Book/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var book = BookConverter.ConvertModelToViewModel(_booksRepository.GetBook(id));

                return View(new ActionBooksPagesViewModel
                {
                    Book = book,
                    Genres = new SelectList(_genresRepository.GetAllGenres(), "Id", "Title",
                                            _genresRepository.GetAllGenres().FirstOrDefault(g => g.Id == book.GenreId))
                });
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Book/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BookViewModel book)
        {
            try
            {
                if (book.Id <= 0)
                    throw new ArgumentException("Book ID is empty or <= 0");
                if (ModelState.IsValid)
                {
                    _booksRepository.DeleteBook(BookConverter.ConvertViewModelToModel(book));
                    return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    return View(book);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}