using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.Infrastructure;
using TestAppAspCore.Models;

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles=RolesHelper.USER_ROLE)]
    public class OrderController : Controller
    {
        [HttpGet]
        public ViewResult Checkout()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                TempData["orderID"] = order.OrderId;
                return RedirectToAction(nameof(HomeController.ShowBooks), nameof(HomeController).Replace("Controller", ""));
            }
            else
                return View(order);

        }
    }
}