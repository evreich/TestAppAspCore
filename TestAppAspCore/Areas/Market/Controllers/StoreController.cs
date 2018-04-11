using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;

namespace TestAppAspCore.Areas.Market.Controllers
{
    [Area("Market")]
    [Authorize(Roles = RolesHelper.USER_ROLE)]
    public class StoreController : Controller
    {
        private readonly IBookOrderRepository _boRepository;

        public StoreController(IBookOrderRepository bookOrderRepository)
        {
            _boRepository = bookOrderRepository;
        }

        public IActionResult ConfirmReturnBook(int bookId)
        {
            _boRepository.ReturnBook(bookId);
            return Json(new { id = bookId, message = "Книга успешно возвращена!" });
        }
    }
}