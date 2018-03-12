using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public interface IGenresRepository
    {
        IEnumerable<Genre> GetAllGenres();
        Genre GetGenre(int id);
        void AddGenre(Genre genre);
        void EditGenre(Genre genre);
        void DeleteGenre(Genre genre);
        IEnumerable<Book> GetAllBookCurrGenre(int id);
    }
}
