using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.Controllers;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Infrastructure;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles=RolesHelper.USER_ROLE)]
    public class OrderController : AbstractOrderController
    {
        public OrderController(IOrdersRepository ordersRepository, IBookOrderRepository bookOrderRepository, IBooksRepository booksRepository) : 
            base(ordersRepository, bookOrderRepository, booksRepository)
        {
        }

        [HttpGet]
        public ViewResult Checkout()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order, [FromServices] UserManager<User> userManager, [FromServices] Cart cart)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = $"Заказ успешно принят и отправлен на рассмотрение менеджеру!";

                //Заполняем доп данные заказа
                order.UserId = (await userManager.FindByNameAsync(User.Identity.Name)).Id;
                
                order.IsSuccess = null;
                order.BookOrders = cart.Lines
                    .Select(line => new BookOrder
                    {
                        BookId = line.Book.Id,
                        CountOfBook = line.Count,
                        Order = order,
                        IsReturned = null
                    }).ToList();

                _ordersRepository.AddOrder(order);
                cart.Clear();
                return RedirectToAction(nameof(HomeController.ShowOrders), nameof(HomeController).Replace("Controller", ""));
            }
            else
                return View(order);

        }
    }
}