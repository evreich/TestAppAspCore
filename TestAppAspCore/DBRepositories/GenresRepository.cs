using Microsoft.EntityFrameworkCore;
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

        public void AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }

        public void EditGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
        }

        public Genre GetGenre(int id)
        {
            return _context.Genres.Include(genre => genre.Books).Single(Genre => Genre.Id == id);
        }

        public List<Genre> GetAllGenres()
        {
            return _context.Genres.Include(genre => genre.Books).ToList();
        }

        public List<Book> GetAllBookCurrGenre(int id)
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
