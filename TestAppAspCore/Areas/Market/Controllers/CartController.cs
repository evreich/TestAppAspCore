using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using TestAppAspCore.Infrastructure;
using TestAppAspCore.ViewModels;
using TestAppAspCore.Areas.Market.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles = RolesHelper.USER_ROLE)]
    public class CartController : Controller
    {
        private readonly IBooksRepository _repository;
        private Cart _cart;

        public CartController(IBooksRepository repo, Cart cartService)
        {
            _repository = repo;
            _cart = cartService;
        }

        [HttpGet]
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int Id, string returnUrl)
        {
            Book book = _repository.GetBook(Id);
            int countBooksInStore = _repository.DecCountBook(book);
            if (book != null)
            {
                _cart.AddItem(book);
            }
            return Json(new { bookId=book.Id, bookCountInStore = countBooksInStore, bookTitle = book.Title, countBooksInCart = _cart.ComputeTotalValue() });
        }

        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int Id, string returnUrl)
        {
            Book book = _repository.GetBook(Id);
            var countReturnedBook = _cart.Lines.Where(line => line.Id == Id).SingleOrDefault().Count;
            _repository.IncCountBooks(book, countReturnedBook);
            if (book != null)
            {              
                _cart.RemoveLine(book);
            }
            return RedirectToAction(nameof(this.Index), new { returnUrl });
        }
    }
}