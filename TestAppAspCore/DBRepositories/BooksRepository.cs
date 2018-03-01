using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.EFCore;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        { 
            return _context.Books.Include(book => book.Genre).ToList();
        }

        public Book GetBook(int id)
        {
            return _context.Books.Include(book => book.Genre).Single(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void EditBook(Book book)
        {
            var currBook = GetBook(book.Id);
            currBook.Title = book.Title;
            currBook.Author = book.Author;
            currBook.GenreId = book.GenreId;
            currBook.DateCreating = book.DateCreating;

            _context.Books.Update(currBook);
            _context.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(GetBook(book.Id));
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
