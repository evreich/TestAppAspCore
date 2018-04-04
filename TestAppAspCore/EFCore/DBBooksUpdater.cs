using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.EFCore
{
    public class DBBooksUpdater : DBUpdater<BooksContext,Book>
    {
        public DBBooksUpdater(BooksContext context):base(context)
        {
        }

        public override void Init()
        {
            if (!_context.Books.Any())
            {
                SetInitBooks(_context.Genres.ToList());
            }
        }

        public override void Update(List<Book> items)
        {
            _context.Books.AddRange(items);
            _context.SaveChanges();
        }

        private List<Book> SetInitBooks(List<Genre> genres)
        {

            List<Book> books = new List<Book>() {
                new Book
                {
                    Title = "Оно",
                    DateCreating = new DateTime(1984,1,1),
                    Author = "Стивен Кинг",
                    Genre = genres[1]
                },
                new Book
                {
                    Title = "Шерлок Холмс",
                    DateCreating = new DateTime(1944,1,1),
                    Author = "Конан Дойль",
                    Genre = genres[0]
                },
                new Book
                {
                    Title = "Виноваты звезды",
                    DateCreating = new DateTime(1999,1,1),
                    Author = "Нора Робертс",
                    Genre = genres[2]
                }
            };
            _context.Books.AddRange(books);
            _context.SaveChanges();
            return books;
        }
    }
}
