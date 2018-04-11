using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;

namespace TestAppAspCore.Controllers
{
    [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," + RolesHelper.BOOKKEEPER_ROLE)]
    public class StoreController : Controller
    {
        private readonly IBookOrderRepository _boRepository;

        public StoreController(IBookOrderRepository bookOrderRepository)
        {
            _boRepository = bookOrderRepository;
        }

        public IActionResult ConfirmReturnBook(int bookId, int countReturnBooks, [FromServices] IBooksRepository booksRepository)
        {
            _boRepository.ConfirmBook(bookId);
            var book = booksRepository.GetBook(bookId);
            booksRepository.IncCountBooks(book, countReturnBooks);

            return Json(new { id = bookId, message = "Книга успешно возвращена в библиотеку!" });
        }

        public IActionResult CancelReturnBook(int bookId)
        {
            _boRepository.CancelBook(bookId);
            return Json(new { id = bookId, message = "Книга возвращена пользователю!" });
        }
    }
}