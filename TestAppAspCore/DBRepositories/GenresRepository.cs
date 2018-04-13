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
    public class GenresRepository : IGenresRepository
    {
        private readonly BooksContext _context;

        public GenresRepository(BooksContext context)
        {
            _context = context;
        }

        public void AddElem(Genre elem)
        {
            _context.Genres.Add(elem);
            _context.SaveChanges();
        }

        public void DeleteElem(Genre elem)
        {
            _context.Genres.Remove(elem);
            _context.SaveChanges();
        }

        public void EditElem(Genre elem)
        {
            _context.Genres.Update(elem);
            _context.SaveChanges();
        }

        public Genre GetElem(int id)
        {
            return _context.Genres.Include(genre => genre.Books).Single(Genre => Genre.Id == id);
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _context.Genres.Include(genre => genre.Books).ToList();
        }

        public IEnumerable<Book> GetAllBookCurrGenre(int id)
        {
            return _context.Genres.Single(genre => genre.Id == id).Books;
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
