using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Book>> GetAllBooks()
        { 
            return await _context.Books.Include(book => book.Genre).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByFilter(string filter, List<Genre> genres)
        {
            List<Book> books = new List<Book>();
            if (genres.FirstOrDefault(genre => genre.Title.ToLower() == filter.ToLower()) != null)
            {
                books = await _context.Books.Include(book => book.Genre).Where(book=> book.Genre.Title.ToLower() == filter.ToLower()).ToListAsync();
            }
            else if (DateTime.TryParse(filter, out DateTime res))
            {
                books = await _context.Books.Include(book => book.Genre).Where(book => book.DateCreating.Year == res.Year).ToListAsync();
            }
            else if (int.TryParse(filter, out int bookYear))
            {
                books = await _context.Books.Include(book => book.Genre).Where(book => book.DateCreating.Year == bookYear).ToListAsync();
            }
            if (!string.IsNullOrEmpty(filter))
            {
                books.AddRange(await _context.Books.Include(book => book.Genre).Where(book => book.Title.ToLower().Contains(filter.ToLower()) || 
                                                                                       book.Author.ToLower().Contains(filter.ToLower())).ToListAsync());
            }

            return books;  
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
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
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
