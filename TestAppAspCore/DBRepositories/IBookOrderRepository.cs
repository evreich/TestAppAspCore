using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    interface IBookOrderRepository
    {
        void ChangeOrderBooks(int orderID, List<Book> deletedBooks);
    }
}
