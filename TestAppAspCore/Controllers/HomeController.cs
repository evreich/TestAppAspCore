using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.ModelHelpers;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace TestAppAspCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly int COUNT_ELEMS_ON_PAGE;
        
        private readonly IBooksRepository _booksRepository;
        private readonly IGenresRepository _genresRepository;

        public HomeController(IBooksRepository booksRepository, IGenresRepository genresRepository, int countElemOnPage = 5)
        {
            _genresRepository = genresRepository;
            _booksRepository = booksRepository;
            COUNT_ELEMS_ON_PAGE = countElemOnPage;
        }

        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," +RolesHelper.STOREKEEPER_ROLE)]
        public async Task<IActionResult> Index(string searchExpr, int? page = 1)
        {
            IEnumerable<Book> books = new List<Book>();
            IndexBookViewModel indexViewModel;
            if (string.IsNullOrEmpty(searchExpr))
            {
                books = await _booksRepository.GetAllBooks();

                indexViewModel = new IndexBookViewModel
                {
                    Books = books.OrderBy(book => book.Title).Skip((page.Value - 1) * COUNT_ELEMS_ON_PAGE)
                            .Take(COUNT_ELEMS_ON_PAGE).Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                    PageViewModel = new PageViewModel(books.Count(), page.Value, COUNT_ELEMS_ON_PAGE),
                };
                return View(indexViewModel);
            }
            else
            {
                books = await _booksRepository.GetBooksByFilter(searchExpr, _genresRepository.GetAllGenres().ToList());
                indexViewModel = new IndexBookViewModel
                {
                    Books = books.OrderBy(book => book.Title).Skip((page.Value - 1) * COUNT_ELEMS_ON_PAGE)
                            .Take(COUNT_ELEMS_ON_PAGE).Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                    PageViewModel = new PageViewModel(books.Count(), page.Value, COUNT_ELEMS_ON_PAGE),
                    SearchExpr = searchExpr
                };
                return View(indexViewModel);
            }
        }

        // GET: Home/ShowGenres
        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE)]
        public IActionResult ShowGenres()
        {
            return View(_genresRepository.GetAllGenres().ToList());
        }

        [Route("Home/ServerError")]
        [AllowAnonymous]
        public ActionResult ServerError()
        {
            return View();
        }
        [AllowAnonymous]
        [Route("Home/ErrorStatusCode")]
        public ActionResult ErrorStatusCode(int code)
        {
            ViewBag.ErrorCode = code;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE)]
        public async Task<IActionResult> ShowUsers([FromServices] UserManager<User> _userManager)
        {
            List<ShowUserViewModel> showUsersList = new List<ShowUserViewModel>();
            foreach (User user in _userManager.Users)
            {
                var role = (await _userManager.GetRolesAsync(user))[0];
                showUsersList.Add(new ShowUserViewModel { User = user, RoleName = role });
            }
            var dasa = HttpContext.Request.Headers["Referer"].ToString();
            ViewBag.PrevPage = "/Home/Index";
            return View(showUsersList);
        }

        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," + RolesHelper.BOOKKEEPER_ROLE)]
        public IActionResult ShowOrders()
        {
            return View();
        }
    }
}