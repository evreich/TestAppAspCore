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
using System;

namespace TestAppAspCore.Controllers
{
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
            }
            else
            {
                books = await _booksRepository.GetBooksByFilter(searchExpr, _genresRepository.GetAllGenres().ToList());
            }
            indexViewModel = new IndexBookViewModel
            {
                Books = _booksRepository.PartBooksForPage(books, page.Value, COUNT_ELEMS_ON_PAGE)
                .Select(book => BookConverter.ConvertModelToViewModel(book)).ToList(),
                PageViewModel = new PageViewModel(books.Count(), page.Value, COUNT_ELEMS_ON_PAGE),
                SearchExpr = String.IsNullOrEmpty(searchExpr) ? string.Empty : searchExpr
                //, ActionName = nameof(this.ShowBooks)
            };
            return View(indexViewModel);
        }

        // GET: Home/ShowGenres
        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," + RolesHelper.STOREKEEPER_ROLE)]
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
        public IActionResult ShowOrders([FromServices] IOrdersRepository ordersRepository)
        {
            return View(ordersRepository.GetNotConfirmedOrders()
                .Select(order => new ShowOrderViewModel
                {
                    OrderID = order.OrderId,
                    DateReturn = order.DateReturn,
                    UserName = order.User.FullName
                })
                .ToList());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RedirectToRoleStartPage([FromServices] UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Count != 1)
                throw new Exception("Кол-во ролей пользователя должно быть равно 1.");
            var startpage = RolesHelper.RoleStartPages.GetValueOrDefault(roles[0]);

            return RedirectPermanent(startpage);
        }

        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," + RolesHelper.BOOKKEEPER_ROLE)]
        public async Task<IActionResult> ShowReturnedBooks([FromServices] IBookOrderRepository boRepository,
                                    [FromServices] UserManager<User> userManager)
        {
            var userID = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
            return View(boRepository.GetReturnedBooks().ToList());
        }
    }
}