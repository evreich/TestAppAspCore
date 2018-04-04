using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.Areas.Market.ViewModels;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.ModelHelpers;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles = RolesConstants.USER_ROLE)]
    public class HomeController : Controller
    {
        private readonly int COUNT_ELEMS_ON_PAGE;

        private readonly IBooksRepository _booksRepository;
        private readonly IGenresRepository _genresRepository;

        public HomeController(IBooksRepository booksRepository, IGenresRepository genresRepository, int countElemOnPage = 8)
        {
            _genresRepository = genresRepository;
            _booksRepository = booksRepository;
            COUNT_ELEMS_ON_PAGE = countElemOnPage;
        }

        [HttpGet]
        public async Task<IActionResult> ShowBooks(string searchExpr, int? page = 1)
        {
            IEnumerable<Book> books = new List<Book>();
            ShowBooksViewModel indexViewModel;
            if (string.IsNullOrEmpty(searchExpr))
            {
                books = await _booksRepository.GetAllBooks();

                indexViewModel = new ShowBooksViewModel
                {
                    Books = books.OrderBy(book => book.Title).Skip((page.Value - 1) * COUNT_ELEMS_ON_PAGE)
                            .Take(COUNT_ELEMS_ON_PAGE).Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                    PageViewModel = new PageViewModel(books.Count(), page.Value, COUNT_ELEMS_ON_PAGE),
                    ActionName = nameof(this.ShowBooks)
                };
                return View(indexViewModel);
            }
            else
            {
                books = await _booksRepository.GetBooksByFilter(searchExpr, _genresRepository.GetAllGenres().ToList());
                indexViewModel = new ShowBooksViewModel
                {
                    Books = books.OrderBy(book => book.Title).Skip((page.Value - 1) * COUNT_ELEMS_ON_PAGE)
                            .Take(COUNT_ELEMS_ON_PAGE).Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                    PageViewModel = new PageViewModel(books.Count(), page.Value, COUNT_ELEMS_ON_PAGE),
                    SearchExpr = searchExpr,
                    ActionName = nameof(this.ShowBooks)
                };
                return View(indexViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowBooksByGenre(string genre, int? page = 1)
        {
            IEnumerable<Book> books = new List<Book>();
            ShowBooksViewModel indexViewModel;
            books = await _booksRepository.GetBooksByGenre(genre);
            indexViewModel = new ShowBooksViewModel
            {
                Books = books.OrderBy(book => book.Title).Skip((page.Value - 1) * COUNT_ELEMS_ON_PAGE)
                        .Take(COUNT_ELEMS_ON_PAGE).Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                PageViewModel = new PageViewModel(books.Count(), page.Value, COUNT_ELEMS_ON_PAGE),
                Genre = genre,
                ActionName = nameof(this.ShowBooksByGenre)
            };
            return View(nameof(ShowBooks),indexViewModel);
        }
    }
}