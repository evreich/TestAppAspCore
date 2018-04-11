using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Controllers
{
    [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," + RolesHelper.BOOKKEEPER_ROLE)]
    public class OrderController : AbstractOrderController
    {
        public OrderController(IOrdersRepository ordersRepository, IBookOrderRepository bookOrderRepository, IBooksRepository booksRepository) :
            base(ordersRepository, bookOrderRepository, booksRepository)
        {
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ConfirmOrder(int orderID)
        {
            _ordersRepository.ConfirmOrder(orderID);
            return Json(new { ID = orderID, message = $"Заказ {orderID} подтвержден!" });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CancelOrder(int orderID)
        {
            _ordersRepository.CancelOrder(orderID);
            //возвращаем книги на склад
            _ordersRepository.GetOrder(orderID)
                .BookOrders.ToList().ForEach((BookOrder bo) =>
                {
                    var book = _booksRepository.GetBook(bo.BookId);
                    _booksRepository.IncCountBooks(book, bo.CountOfBook);
                });
            return Json(new { ID = orderID, message = $"Заказ {orderID} отменен!" });
        }
    }
}