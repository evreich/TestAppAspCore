using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public interface IBooksRepository : ICRUDRepository<Book>, IDisposable
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetBooksByFilter(string filter, List<Genre> genres);
        Task<IEnumerable<Book>> GetBooksByFilter(string filter, string genreTitle);
        Task<IEnumerable<Book>> GetBooksByGenre(string genreTitle);
        void IncCountBooks(Book book);
        void IncCountBooks(Book book, int count);
        int DecCountBook(Book book);
        int DecCountBook(Book book, int count);
        List<Book> PartBooksForPage(IEnumerable<Book> books, int page, int CountElemsOnPage);
    }
}
