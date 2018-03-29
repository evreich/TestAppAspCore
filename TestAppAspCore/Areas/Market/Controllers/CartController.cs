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

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    public class CartController : Controller
    {
        private IBooksRepository _repository;
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
            if (book != null)
            {
                _cart.AddItem(book);
            }
            return Json(new { bookTitle = book.Title, countBooks = _cart.ComputeTotalValue() });
        }

        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int Id, string returnUrl)
        {
            Book book = _repository.GetBook(Id);
            if (book != null)
            {              
                _cart.RemoveLine(book);
            }
            return RedirectToAction(nameof(this.Index), new { returnUrl });
        }
    }
}