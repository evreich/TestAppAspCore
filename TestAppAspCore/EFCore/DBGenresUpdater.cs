using QPD.DBUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.EFCore
{
    public class DBGenresUpdater : DBUpdater<BooksContext, Genre>
    {
        public DBGenresUpdater(BooksContext context) : base(context)
        {
        }

        public override void Init()
        {
            if (!_context.Genres.Any())
            {
                SetInitGenres();
            }
        }

        public override void Update(List<Genre> items)
        {
            _context.Genres.AddRange(items);
            _context.SaveChanges();
        }

        private List<Genre> SetInitGenres()
        {
            List<Genre> genres = new List<Genre>() {
                new Genre
                {
                    Title = "Детектив"
                },
                new Genre
                {
                    Title = "Фантастика"
                },
                new Genre
                {
                    Title = "Роман"
                },
                new Genre
                {
                    Title = "Антиутопия"
                },
                new Genre
                {
                    Title = "Приключения"
                }

            };
            _context.Genres.AddRange(genres);
            _context.SaveChanges();
            return genres;
        }
    }
}
