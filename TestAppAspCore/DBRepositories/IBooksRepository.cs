using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Book GetBook(int id);
        void AddBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
        Task<IEnumerable<Book>> GetBooksByFilter(string filter, List<Genre> genres);
    }
}
