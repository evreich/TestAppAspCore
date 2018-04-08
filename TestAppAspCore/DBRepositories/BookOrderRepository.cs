using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.EFCore;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public class BookOrderRepository : IBookOrderRepository
    {
        private BooksContext _context;

        public BookOrderRepository(BooksContext context)
        {
            _context = context;
        }

        public void ChangeOrderBooks(int orderID, List<Book> deletedBooks)
        {
         
        }

        public List<Book> GetBooksOfOrder(int orderID)
        {
            throw new NotImplementedException();
        }
    }
}
