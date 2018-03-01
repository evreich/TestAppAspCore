using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace QPD.DBUpdaters
{
    public abstract class DBUpdater<T, U> where T : DbContext where U: DBModel
    {
        protected readonly T _context;
        public DBUpdater(T context)
        {
            _context = context;
        }

        public abstract void Init();

        public abstract void Update(List<U> items);
    }
}
