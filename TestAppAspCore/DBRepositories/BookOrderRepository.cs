using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.EFCore;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public class BookOrderRepository : IBookOrderRepository
    {
        private readonly BooksContext _context;

        public BookOrderRepository(BooksContext context)
        {
            _context = context;
        }

        public void SetActualBooksForOrder(int orderID, List<BookOrder> actualBooks)
        {
            //получаем список книг текущего заказа
            List<BookOrder> booksOrder = _context.BookOrder
                                            .Where(elem => elem.OrderId == orderID)
                                            .ToList();
            //оставляем в нем книги, которые требуется удалить
            booksOrder.RemoveAll(bo => actualBooks
                                            .Select(b => b.BookId)
                                            .Contains(bo.BookId));
            //удаляем книги из сущности
            _context.BookOrder.RemoveRange(booksOrder);
            _context.SaveChanges();
        }

        public IEnumerable<BookOrder> GetBooksOfOrder(int orderID)
        {
            return _context.BookOrder
                .Where(elem => elem.OrderId == orderID)
                .Include(elem => elem.Book)
                .ToList();
        }

        public IEnumerable<Book> GetBooksForUser(string userID, bool? isReturned)
        {
            return from users in _context.Users
                   join orders in _context.Orders on users.Id equals orders.UserId
                   join bo in _context.BookOrder on orders.OrderId equals bo.OrderId
                   join books in _context.Books on bo.BookId equals books.Id
                   join genres in _context.Genres on books.GenreId equals genres.Id
                   where users.Id == userID && orders.IsSuccess == true && bo.IsReturned == isReturned
                   select new Book
                   {
                       Id = books.Id,
                       Author = books.Author,
                       DateCreating = books.DateCreating,
                       Genre = genres,
                       Title = books.Title,
                       GenreId = genres.Id,
                       Count = bo.CountOfBook
                   };
        }

        public IEnumerable<Book> GetReturnedBooks()
        {
            return from users in _context.Users
                   join orders in _context.Orders on users.Id equals orders.UserId
                   join bo in _context.BookOrder on orders.OrderId equals bo.OrderId
                   join books in _context.Books on bo.BookId equals books.Id
                   join genres in _context.Genres on books.GenreId equals genres.Id
                   where orders.IsSuccess == true && bo.IsReturned == true
                   select new Book
                   {
                       Id = books.Id,
                       Author = books.Author,
                       DateCreating = books.DateCreating,
                       Genre = genres,
                       Title = books.Title,
                       GenreId = genres.Id,
                       Count = bo.CountOfBook
                   };
        }

        public void ReturnBook(int bookID)
        {
            var book = _context.BookOrder.Where(b => b.BookId == bookID).SingleOrDefault();
            book.IsReturned = true;
            _context.Update(book);
            _context.SaveChanges();
        }

        public void ConfirmBook(int bookID)
        {
            var book = _context.BookOrder.Where(b => b.BookId == bookID).SingleOrDefault();
            _context.BookOrder.Remove(book);
            _context.SaveChanges();
        }

        public void CancelBook(int bookID)
        {
            var book = _context.BookOrder.Where(b => b.BookId == bookID).SingleOrDefault();
            book.IsReturned = false;
            _context.Update(book);
            _context.SaveChanges();
        }
    }
}
