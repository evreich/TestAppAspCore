using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QPD.DBUpdaters;
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

        public List<Book> PartBooksForPage(IEnumerable<Book> books, int page, int CountElemsOnPage)
        {
            return books.OrderBy(book => book.Title).Skip((page - 1) * CountElemsOnPage).Take(CountElemsOnPage).ToList();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        { 
            return await _context.Books.Include(book => book.Genre).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenre(string genreTitle)
        {
            return await _context.Books.Include(book => book.Genre).Where(book => book.Genre.Title.ToLower() == genreTitle.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByFilter(string filter, List<Genre> genres)
        {
            List<Book> books = new List<Book>();
            if (genres.FirstOrDefault(genre => genre.Title.ToLower() == filter.ToLower()) != null)
            {
                books = await _context
                    .Books
                    .Include(book => book.Genre)
                    .Where(book=> book.Genre.Title.ToLower() == filter.ToLower())
                    .ToListAsync();
            }
            else if (DateTime.TryParse(filter, out DateTime res))
            {
                books = await _context.Books.Include(book => book.Genre).Where(book => book.DateCreating.Year == res.Year).ToListAsync();
            }
            else if (int.TryParse(filter, out int bookYear))
            {
                books = await _context.Books.Include(book => book.Genre).Where(book => book.DateCreating.Year == bookYear).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(filter))
            {
                books.AddRange(await _context.Books.Include(book => book.Genre).Where(book => book.Title.ToLower().Contains(filter.ToLower()) || 
                                                                                       book.Author.ToLower().Contains(filter.ToLower())).ToListAsync());
            }

            return books;  
        }

        public async Task<IEnumerable<Book>> GetBooksByFilter(string filter, string genreTitle)
        {
            IEnumerable<Book> books = await GetBooksByGenre(genreTitle);

            if (DateTime.TryParse(filter, out DateTime res))
            {
                return books.Where(book => book.DateCreating.Year == res.Year).ToList();
            }
            else if (int.TryParse(filter, out int bookYear))
            {
                return books.Where(book => book.DateCreating.Year == bookYear).ToList();
            }
            else if (!string.IsNullOrEmpty(filter))
            {
                return books.Where(book => book.Title.ToLower().Contains(filter.ToLower()) || book.Author.ToLower().Contains(filter.ToLower()))
                    .ToList();
            }
            else
                return new List<Book>();
        }

        public Book GetElem(int id)
        {
            return _context.Books.Include(book => book.Genre).Single(book => book.Id == id);
        }

        public void AddElem(Book elem)
        {
            _context.Books.Add(elem);
            _context.SaveChanges();
        }

        public void EditElem(Book elem)
        {
            _context.Books.Update(elem);
            _context.SaveChanges();
        }

        public void DeleteElem(Book elem)
        {
            _context.Books.Remove(elem);
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

        public void IncCountBooks(Book book)
        {
            book.Count++;
            _context.Update(book);
            _context.SaveChanges();
        }

        public void IncCountBooks(Book book, int count)
        {
            book.Count+=count;
            _context.Update(book);
            _context.SaveChanges();
        }

        public int DecCountBook(Book book)
        {
            book.Count--;
            _context.Update(book);
            _context.SaveChanges();
            return book.Count;
        }

        public int DecCountBook(Book book, int count)
        {
            if (book.Count < count)
                throw new ArgumentException("Кол-во удаляемых книг превышает кол-во имеющихся");
            book.Count -= count;
            _context.Update(book);
            _context.SaveChanges();
            return book.Count;
        }
    }
}
