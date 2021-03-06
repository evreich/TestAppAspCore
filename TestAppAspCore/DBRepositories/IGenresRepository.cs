﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public interface IGenresRepository : ICRUDRepository<Genre>, IDisposable
    {
        IEnumerable<Genre> GetAllGenres();
        IEnumerable<Book> GetAllBookCurrGenre(int id);
    }
}
