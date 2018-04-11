﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public interface IBookOrderRepository
    {
        void SetActualBooksForOrder(int orderID, List<BookOrder> actualBooks);
        IEnumerable<BookOrder> GetBooksOfOrder(int orderID);
        IEnumerable<Book> GetBooksForUser(string userID, bool? isReturned);
        IEnumerable<Book> GetReturnedBooks();
        void ReturnBook(int bookID);
        void ConfirmBook(int bookID);
        void CancelBook(int bookID);
    }
}
