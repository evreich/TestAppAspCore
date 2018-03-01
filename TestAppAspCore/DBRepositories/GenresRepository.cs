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

        public void AddGenres(Genre genre)
        {
            throw new NotImplementedException();
        }

        public void DeleteGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public void EditGenre(Genre genre)
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public Genre GetGenre(int id)
        {
            throw new NotImplementedException();
        }
    }
}
