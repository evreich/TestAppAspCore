using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using TestAppAspCore.Infrastructure;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    public class CartController : Controller
    {
        private IBooksRepository repository;
        public CartController(IBooksRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public RedirectToActionResult AddToCart(int Id, string returnUrl)
        {
            Book book = repository.GetBook(Id);
            if (book != null)
            {
                Cart cart = GetCart();
                cart.AddItem(book, 1);
                SaveCart(cart);
            }
            return RedirectToAction(nameof(this.Index), new { returnUrl });
        }

        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int Id, string returnUrl)
        {
            Book book = repository.GetBook(Id);
            if (book != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(book);
                SaveCart(cart);
            }
            return RedirectToAction(nameof(this.Index), new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}