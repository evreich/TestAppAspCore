using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.EFCore;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BooksContext _context;

        public OrdersRepository(BooksContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();          
        }

        private void ChangeCountBooksOnStore(List<BookOrder> booksOrder)
        {
            booksOrder.ForEach((BookOrder bo) =>
            {
                var book = _context.Books.Where(b => b.Id == bo.BookId).SingleOrDefault();
                if (book.Count < bo.CountOfBook)
                    throw new Exception("Кол-во книг на складе меньше кол-ва книг для пользователя");
                book.Count -= bo.CountOfBook;
                _context.Update(book);                               
            });
            _context.SaveChanges();
        }

        public void CancelOrder(int orderID)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderID);
            order.IsSuccess = false;
            _context.Update(order);
            _context.SaveChanges();
        }

        public void ConfirmOrder(int orderID)
        {
            var order = _context.Orders.Include(o=> o.BookOrders).SingleOrDefault(o => o.OrderId == orderID);
            order.IsSuccess = true;
            _context.Update(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetNotConfirmedOrders()
        {
            return _context.Orders
                .Where(o => o.IsSuccess == null)
                .Include(o => o.User);
        }

        public Order GetOrder(int id)
        {
            return _context.Orders
                .Include(o => o.BookOrders)
                .SingleOrDefault(o => o.OrderId == id);
        }

        public IEnumerable<Order> GetOrdersForUser(string userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.User);
        }

        public void EditOrder(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
        }


    }
}
