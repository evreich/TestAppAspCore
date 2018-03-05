using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.ModelHelpers
{
    internal static class BookConverter
    {
        public static Book ConvertViewModelToModel(BookViewModel bookVM)
        {
            return new Book
            {
                Id = bookVM.Id,
                Title = bookVM.Title,
                Author = bookVM.Author,
                GenreId = bookVM.GenreId,
                DateCreating = bookVM.DateCreating
            };
        }

        public static BookViewModel ConvertModelToViewModel(Book book)
        {
            return new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                GenreId = book.GenreId,
                GenreTitle = book.Genre.Title,
                DateCreating = book.DateCreating
            };
        }
    }
}
