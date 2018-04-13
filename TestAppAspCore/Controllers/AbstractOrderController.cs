using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Controllers
{
    public abstract class AbstractOrderController : Controller
    {
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly IBookOrderRepository _boRepository;
        protected readonly IBooksRepository _booksRepository;

        public AbstractOrderController(IOrdersRepository ordersRepository, 
            IBookOrderRepository bookOrderRepository, IBooksRepository booksRepository)
        {
            _ordersRepository = ordersRepository;
            _boRepository = bookOrderRepository;
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult ShowOrder(int orderID)
        {
            var order = _ordersRepository.GetOrder(orderID);
            var booksOfOrder = _boRepository.GetBooksOfOrder(orderID)
                                .Select(bo => new BookOfOrder
                                {
                                    Id = bo.BookId,
                                    Title = bo.Book.Title,
                                    Count = bo.CountOfBook
                                })
                                .ToArray();
            return View(new OrderViewModel
            {
                Order = order,
                BooksOfOrder = booksOfOrder
            });
        }

        [HttpGet]
        public IActionResult EditOrder(int orderID)
        {
            var order = _ordersRepository.GetOrder(orderID);
            var booksOfOrder = _boRepository.GetBooksOfOrder(orderID)
                                .Select(bo => new BookOfOrder
                                {
                                    Id = bo.BookId,
                                    Title = bo.Book.Title,
                                    Count = bo.CountOfBook
                                })
                                .ToArray();

            return View(new OrderViewModel
            {
                Order = order,
                BooksOfOrder = booksOfOrder
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditOrder(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var order = orderVM.Order;
                //получаем список книг, которые остаются в заказе
                var curr_books = orderVM.BooksOfOrder
                                .Where(bo => bo.Selected)
                                .Select(bo =>
                                new BookOrder
                                {
                                    BookId = bo.Id,
                                    CountOfBook = bo.Count,
                                    OrderId = order.OrderId
                                }).ToList();
                //добавляем их в заказ
                curr_books.ForEach((BookOrder bo) =>
                {
                    order.BookOrders.Add(bo);
                });
                //устанавливаем в промежуточную таблицу
                _boRepository.SetActualBooksForOrder(order.OrderId, curr_books);
                //затем сохраняем остальные изменения в заказе
                _ordersRepository.EditOrder(order);

                //получаем список книг, которые требуется вернуть на склад
                var returned_books = orderVM.BooksOfOrder
                .Where(bo => !bo.Selected)
                .Select(bo =>
                new BookOrder
                {
                    BookId = bo.Id,
                    CountOfBook = bo.Count,
                }).ToList();
                //возвращаем на склад
                returned_books.ForEach((BookOrder bo) =>
                {
                    var book = _booksRepository.GetElem(bo.BookId);
                    _booksRepository.IncCountBooks(book, bo.CountOfBook);
                });

                TempData["message"] = $"Заказ \"{order.OrderId}\" успешно изменен!";
                return RedirectToAction(nameof(HomeController.ShowOrders), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(orderVM);
            }
        }
    }
}